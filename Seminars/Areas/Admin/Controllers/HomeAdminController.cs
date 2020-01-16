using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.ViewModel;

namespace Seminars.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "admin")]
    public class HomeAdminController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IUserValidator<AppUser> _userValidator;
        private readonly IPasswordValidator<AppUser> _passwordValidator;
        private readonly IPasswordHasher<AppUser> _passwordHasher;
        public HomeAdminController(UserManager<AppUser> usMgr, IUserValidator<AppUser> userValidator, IPasswordValidator<AppUser> passwordValidator, IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = usMgr;
            _userValidator = userValidator;
            _passwordValidator = passwordValidator;
            _passwordHasher = passwordHasher;
        }

        [Authorize]
        public ViewResult TestAuth() => View(new Dictionary<string, object> { ["placeholder"] = "Placeholder"});

        public IActionResult Index()
        {
            return View();
        }

        public ViewResult Users() {
            return View(_userManager.Users);
        }

        public ViewResult CreateUser() => View();

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = new AppUser
            {
                UserName = model.Name,
                Email = model.Email,
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                return RedirectToAction(nameof(Users));
            else
                AddErrorsFromResult(result);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser (string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (User != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Users");
                else
                    AddErrorsFromResult(result);
            }
            else
                ModelState.AddModelError("", "User Not Found");

            return View("Users", _userManager.Users);
        }

        public async Task<IActionResult> EditUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
                return View(user);
            else
                return RedirectToAction("Users");
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(string id, string email, string password)
        {
            var user = await _userManager.FindByIdAsync(id);
            if(user != null) {
                user.Email = email;
                var validEmail = await _userValidator.ValidateAsync(_userManager, user);

                if (!validEmail.Succeeded)
                    AddErrorsFromResult(validEmail);

                IdentityResult validPass = null;
                if (!string.IsNullOrEmpty(password))
                {
                    validPass = await _passwordValidator.ValidateAsync(_userManager, user, password);
                    if (validPass.Succeeded)
                        user.PasswordHash = _passwordHasher.HashPassword(user, password);
                    else
                        AddErrorsFromResult(validPass);
                }
                if ((validEmail.Succeeded && validPass == null) || (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(Users));
                    else
                        AddErrorsFromResult(result);
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}