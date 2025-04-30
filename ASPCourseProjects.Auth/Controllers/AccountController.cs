using ASPCourseProjects.Auth.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ASPCourseProjects.Auth.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> usermanager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = usermanager;
            _signInManager = signInManager;
        }

        // Register - GET
        public IActionResult Register()
        {
            return View();
        }

        // Register - POST
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var identityUser = new IdentityUser
            {
                UserName = model.Username,
                Email = model.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, model.Password);
            if (identityResult.Succeeded)
            {
                var roleIdentityResult = await _userManager.AddToRoleAsync(identityUser, "User");
                if (roleIdentityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Register");
            }
            return View();
        }

        // Login - GET
        public IActionResult Login()
        {
            return View();
        }

        // Login - POST
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
            if (result != null && result.Succeeded)
            {
                return RedirectToAction("LoginSuccess", "Account");
            }
            ModelState.AddModelError("", "Invalid login attempt.");
            return View();
        }

        // Login Success
        public IActionResult LoginSuccess()
        {
            return View();
        }

        // Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Admin()
        {
            return View();
        }
    }
}
