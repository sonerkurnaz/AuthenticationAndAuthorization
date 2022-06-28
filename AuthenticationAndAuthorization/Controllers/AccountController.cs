using AuthenticationAndAuthorization.Models.DTOs;
using AuthenticationAndAuthorization.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        // AspNetUsers Tablosunun manager sinifidir. 
        // Kisaca user'lari yöneten siniftir.
        private readonly UserManager<AppUser> userManager;
        private readonly SignInManager<AppUser> signInManager;
        private readonly IPasswordHasher<AppUser> passwordHasher;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IPasswordHasher<AppUser> passwordHasher)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.passwordHasher = passwordHasher;
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

        [AllowAnonymous]
        public IActionResult Login(string url)
        {

            return View(new LoginDTO { ReturnUrl = url });
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(loginDTO.UserName);
                if (user != null)
                {
                    var signInResult = await signInManager.PasswordSignInAsync(user, loginDTO.Password, false, false);

                    if (signInResult.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }

                    ModelState.AddModelError("", "Kullanici Adi yada Şifre Yanliş");

                }

            }

            return View(loginDTO);
        }
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);

            UserUpdateDTO userUpdateDTO = new UserUpdateDTO(user);

            return View(userUpdateDTO);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserUpdateDTO userUpdateDTO)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByNameAsync(User.Identity.Name);
                user.UserName = userUpdateDTO.UserName;
                if (userUpdateDTO.Password != null)
                {
                    user.PasswordHash = passwordHasher.HashPassword(user, userUpdateDTO.Password);
                }
                var result = await userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(userUpdateDTO);
        }
    }
}
