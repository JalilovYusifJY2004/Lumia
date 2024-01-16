
using LumiaPraktika.Areas.Admin.ViewModels;
using LumiaPraktika.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LumiaPraktika.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager,RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
           _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }
            userVM.Name = userVM.Name.Trim();
            userVM.Surname= userVM.Surname.Trim();
            string name = Char.ToUpper(userVM.Name[0]) + userVM.Name.Substring(1).ToLower();
            string surname= Char.ToUpper(userVM.Surname[0])+userVM.Surname.Substring(1).ToLower();
            AppUser user = new()
            {
                Name = name,
                Surname = surname,
                Email = userVM.Email,
                UserName = userVM.Name

            };

            IdentityResult result= await _userManager.CreateAsync(user,userVM.Password);
            if (!result.Succeeded) 
            {
                foreach (IdentityError  error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(userVM);
            }
    
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }
        
    }
}
