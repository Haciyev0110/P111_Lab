using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.Models;
using Academic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Academic.Areas.AcademicArea.Controllers
{
    [Area("AcademicArea")]

    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginVm {ReturnUrl=returnUrl });


        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm vmmodel)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(vmmodel.Email);

                if (user != null)
                {
                    // проверяем, принадлежит ли URL приложению

                    await _signInManager.SignOutAsync();
                    if ((await _signInManager.PasswordSignInAsync(user, vmmodel.Password, false, false)).Succeeded)
                    {

                        return Redirect( vmmodel?.ReturnUrl ?? "/OrenGeartAdmin/Dashboard/Index");
                    }
                }
            }
            ModelState.AddModelError("", "Invalid email or password");

            return View(vmmodel);
        }

        public async Task<IActionResult> Logout(string returnUrl = "/")
        {
            await _signInManager.SignOutAsync();
            return Redirect(returnUrl);

        }


    }
}
