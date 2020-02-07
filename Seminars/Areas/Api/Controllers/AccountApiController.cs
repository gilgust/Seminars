using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.ViewModel;

namespace Seminars.Areas.Api.Controllers
{
    [Route("api/Account")]
    [Authorize]
    [ApiController]
    public class AccountApiController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountApiController(UserManager<AppUser> userMgr, SignInManager<AppUser> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginModel details, string returnUrl)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userManager.FindByEmailAsync(details.Email);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                var result = await _signInManager.PasswordSignInAsync(user, details.Password, false, false);

                if (result.Succeeded)
                    return Ok(returnUrl ?? "/");
            }

            ModelState.AddModelError(nameof(LoginModel.Email), "Invalid user or password");

            return BadRequest(ModelState);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Logup(LogupModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

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
                return Ok(user);
            }
            else
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            
            return BadRequest(ModelState);
        }
    }
}
