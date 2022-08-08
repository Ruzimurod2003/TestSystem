using Functions;
using Functions.PasswordHash;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;
using SqlServer_Database.Repository;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System;
using Microsoft.AspNetCore.Authentication;

namespace TestSystem.Controllers
{
    // [Authorize]
    public class RegisterController : Controller
    {
        private readonly ISqlOperation sqlOperation;

        public RegisterController(ISqlOperation _sqlOperation)
        {
            sqlOperation = _sqlOperation;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(RegisterIndexViewModel viewModel)
        {
            bool result = false; bool checkbyEmail = true;
            string? errorMessageEmail; string? errorMessagePassword;
            if (EmailChecked.ValidateEmail(viewModel.Email ?? "", out errorMessageEmail))
            {
                if (StrongPasswordValidation.ValidatePassword(viewModel.Password ?? "", out errorMessagePassword))
                {
                    if (ModelState.IsValid)
                    {
                        checkbyEmail = sqlOperation.FindByEmail(viewModel.Email ?? "");
                        if (!checkbyEmail)
                        {
                            User new_user = new User
                            {
                                Id = Guid.NewGuid(),
                                FirstName = viewModel.FirstName,
                                LastName = viewModel.LastName,
                                Birth = viewModel.Birth,
                                Email = viewModel.Email,
                                HashPassword = SecurePasswordHasher.Hash(viewModel.Password ?? ""),
                                RoleId = Guid.Parse("86ef4afe-bc06-4926-bc1a-f800c8c219dc")
                            };
                            result = sqlOperation.AddUser(new_user);
                        }
                        else
                        {
                            ModelState.AddModelError("", "Such a user exists in the system");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", errorMessagePassword);
                }
            }
            else
            {
                ModelState.AddModelError("", errorMessageEmail);
            }
            if (result)
            {
                //FormsAuthentication.SetAuthCookie(model.Name, true);
                return RedirectToAction("Index", "Home");
            }
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginAsync(RegisterLoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                string? email = viewModel.Email;
                string? password = viewModel.Password;
                bool rememberMy = viewModel.RememberMe;
                User? user = sqlOperation.GetUserByEmail(email ?? "");
                if (!SecurePasswordHasher.Verify(password ?? "", user?.HashPassword ?? ""))
                {
                    ModelState.AddModelError("", "Password error!!!!");
                    return View(viewModel);
                }
                string? role = sqlOperation.GetRole(user?.RoleId ?? new Guid()).RoleName;
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, email??""),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, role??"")
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
               // await context.SignInAsync(claimsPrincipal);
                return RedirectToRoute("/" + role + "/index");
            }
            return View(viewModel);
        }
    }
}
