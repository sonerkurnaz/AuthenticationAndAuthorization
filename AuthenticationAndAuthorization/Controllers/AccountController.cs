using AuthenticationAndAuthorization.Models.DTOs;
using AuthenticationAndAuthorization.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IPasswordHasher<AppUser> password;

        public AccountController(UserManager<AppUser>
            userManager, SignInManager<AppUser> signInManager,
            IPasswordHasher<AppUser> password)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.password = password;
        }
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public IActionResult Register()
        {
            RegisterDTO registerDTO = new();
            return View(registerDTO);
        }
        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser appUser = new AppUser { UserName = registerDTO.UserName, Email = registerDTO.Email };
                var result = await userManager.CreateAsync(appUser, registerDTO.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(registerDTO);
        }
    }
}
