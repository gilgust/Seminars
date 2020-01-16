using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr ) 
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (!ModelState.IsValid) return View(details);

            var user = await _userManager.FindByEmailAsync(details.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);

                if (result.Succeeded)
                {
                    return Redirect(returnUrl ?? "/");
                }
            }
            ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");
            return View(details);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ViewResult Logup() => View();

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logup(LogupModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                // var result = await _userManager.CreateAsync(user, model.Password);

                // if (result.Succeeded)
                //     return RedirectToAction("Index", "Home");
                // else
                //     foreach (var error in result.Errors)
                //         ModelState.AddModelError("", error.Description);
            }
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult AccessDenied() => View();

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> NameIsAvailable(string name)
        {
            var user = await _userManager.FindByNameAsync(name);
            return Json(user == null);
        }
        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> EmailIsAvailable(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Json(user == null);
        }
    }
}