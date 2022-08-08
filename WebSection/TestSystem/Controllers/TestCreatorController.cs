using Microsoft.AspNetCore.Mvc;
using Models.ViewModel;
using SqlServer_Database.Repository;

namespace TestSystem.Controllers
{
    public class TestCreatorController : Controller
    {
        private readonly ISqlOperation sqlOperation;

        public TestCreatorController(ISqlOperation _sqlOperation)
        {
            sqlOperation = _sqlOperation;
        }
        public IActionResult Index()
        {
            List<string> DepartmentNames = sqlOperation.GetDepartmentNames();
            TestCreatorViewModel viewModel = new TestCreatorViewModel
            {
                DepartmentNames = DepartmentNames
            };
            return View(viewModel);
        }
        [HttpGet]
        public IActionResult CreateProblem()
        {
            return View();
        }
        public IActionResult CreateProblem([FromBody] TestCreatorViewModel viewModel)
        {
            return View(viewModel);
        }
        public IActionResult EditProblem(Guid ProblemId, TestCreatorViewModel viewModel)
        {
            return View();
        }
        public IActionResult DeleteProblem(Guid ProblemId)
        {
            return View();
        }
    }
}