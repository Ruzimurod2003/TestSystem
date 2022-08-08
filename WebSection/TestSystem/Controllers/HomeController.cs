using Microsoft.AspNetCore.Mvc;
using Models;
using Models.ViewModel;
using SqlServer_Database.Repository;

namespace TestSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISqlOperation sqlOperation;
        public HomeController(ISqlOperation _sqlOperation)
        {
            sqlOperation = _sqlOperation;
        }
        [HttpGet]
        public IActionResult Index()
        {
            int i = 1;
            List<User> users = sqlOperation.GetUsers();
            List<HomeIndexViewModel> users_1 = new List<HomeIndexViewModel>();
            foreach (var item in users)
            {
                string? role = sqlOperation.GetRole(item.RoleId).RoleName;
                HomeIndexViewModel viewModel = new HomeIndexViewModel
                {
                    UserId = item.Id,
                    Email = item.Email,
                    Index = i,
                    Birth = item.Birth,
                    CreatedDate = item.CreatedDate,
                    FullName = item.FirstName + " " + item.LastName,
                    RoleString = role
                };
                i++;
                users_1.Add(viewModel);
            }
            return View(users_1);
        }
        public IActionResult User(string id)
        {
            var user = sqlOperation.GetUserById(Guid.Parse(id));
            HomeUserViewModel viewModel = new HomeUserViewModel
            {
                FullName = user.FirstName + " " + user.LastName,
                Birth = user.Birth,
                CreatedDate = user.CreatedDate,
                Email = user.Email,
                UserId = user.Id,
                RoleString = sqlOperation.GetRole(user.RoleId).RoleName
            };
            return View(viewModel);
        }
        [HttpPost]
        public IActionResult User(HomeUserViewModel viewModel)
        {
            var user = sqlOperation.GetUserById(viewModel.UserId);
            Guid oldrole = user.RoleId;
            Guid newRole = sqlOperation.GetRoleId(viewModel?.RoleString ?? "");
            if (oldrole == newRole)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                bool res = sqlOperation.UpdateRoleInUser(user.Id, newRole);
                if (res)
                {

                    return RedirectToAction("Index", "Home");
                }
                return View(viewModel);
            }
        }
    }
}
