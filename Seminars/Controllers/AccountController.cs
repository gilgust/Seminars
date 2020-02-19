using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.ViewModel;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Seminars.security;

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
        public IActionResult Login(string returnUrl = null)
        {
            if (_signInManager.IsSignedIn(User)) return Redirect(returnUrl);

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
            return Redirect("/");


            // return RedirectToAction("Index", "Home", new { area = "" });
        }

        [AllowAnonymous]
        public ViewResult Logup(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logup(LogupModel model, string returnUrl)
        {
             ViewBag.returnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Name,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    if (User?.Identity?.IsAuthenticated ?? false)
                        await _signInManager.SignOutAsync();

                    await _userManager.AddToRoleAsync(user, "user");

                    await _signInManager.PasswordSignInAsync(user, model.Password, false, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("", error.Description);
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



        //test jwt (lack a big dady)

        [HttpPost("/token")]
        [AllowAnonymous]
        public IActionResult Token(string username, string password)
        {
            var identity = GetIdentityAsync(username, password).Result;
            if (identity == null)
                return BadRequest(new {errorText = "Invalid username or password"});

            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                notBefore: now,
                claims:identity.Claims,
                expires:now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Json(response);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            if (user == null) return null;

            await _signInManager.SignOutAsync();
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

            return result.Succeeded 
                ? new ClaimsIdentity(
                    User.Claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType)
                : null;
        }
    }
}