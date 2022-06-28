using AuthenticationAndAuthorization.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorization.Controllers
{
    //[Authorize(Roles = "admin")]
    //[Authorize(Roles = "manager")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(roleManager.Roles.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (ModelState.IsValid)
            {
                var result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Role");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View();
        }
        public async Task<IActionResult> AssignedUsers(string id)
        {
            IdentityRole identityRole = await roleManager.FindByIdAsync(id);
            List<AppUser> hasRole = new List<AppUser>();
            List<AppUser> hasNotRole = new List<AppUser>();

            foreach (AppUser user in userManager.Users)
            {
                //var list = await userManager.IsInRoleAsync(user, identityRole.Name) ? hasRole : hasNotRole;
                //list.Add(user);
                bool sonuc = await userManager.IsInRoleAsync(user, identityRole.Name);
                if (sonuc)
                {
                    hasRole.Add(user);
                }
                else
                {
                    hasNotRole.Add(user);
                }

            }
        }
    }
}