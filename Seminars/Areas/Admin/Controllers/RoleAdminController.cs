using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Seminars.Models;
using Seminars.ViewModel;

namespace Seminars.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "admin")]
    public class RoleAdminController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleAdminController(RoleManager<AppRole> roleMgr, UserManager<AppUser> userMgr)
        {
            _roleManager = roleMgr;
            _userManager = userMgr;
        }

        public ViewResult Index() {
            var roles = _roleManager.Roles;
            return View(roles);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(new AppRole(name));
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else 
                    AddErrorsFromResult(result);
            }
            return View(nameof(Create), model: name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role != null)
            {
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    AddErrorsFromResult(result);
            }
            else
                ModelState.AddModelError("", "No role found");

            return View(nameof(Index), _roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var members = new List<AppUser>();
            var nonMembers = new List<AppUser>();

            foreach (var user in _userManager.Users)
            {
                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }

            return View(new RoleEditModel {
                Role = role,
                Members = members,
                NonMembers =  nonMembers
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result;
                foreach (var userId in model.IdsToAdd ?? new string[]{ })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null) continue;

                    result = await _userManager.AddToRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        AddErrorsFromResult(result);
                    }
                }

                foreach (var userId in model.IdsToDelete ?? new string[] { })
                {
                    var user = await _userManager.FindByIdAsync(userId);
                    if (user == null) continue;

                    result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
                    if (!result.Succeeded)
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }

            if (ModelState.IsValid) 
                return RedirectToAction(nameof(Index));
            else 
                return await Edit(model.RoleId);
            
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
