using ASPCourseProjects.Auth.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASPCourseProjects.Auth.Controllers
{
    public class AdminUserController : Controller
    {
        private readonly IUserRepo _userRepo;

        public AdminUserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UserList()
        {
            var users = await _userRepo.UserList();
            return View(users);
        }
    }
}
