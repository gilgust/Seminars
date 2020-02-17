using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.Repositories;
using Seminars.infrastructure;
using Seminars.ViewModel;

namespace Seminars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeminarAdminController : Controller
    {
        private readonly ISeminarRepository _repository;
        private readonly RoleManager<AppRole> _roleManager;

        public SeminarAdminController(ISeminarRepository repository, RoleManager<AppRole> roleManager)
        {
            _repository = repository;
            _roleManager = roleManager;
        }

        public ViewResult Index() => View(_repository.Seminars.ToList());
        

        public ViewResult Edit(int seminarId)
        {
            var editModel = new EditSeminarViewModel
            {
                Seminar = _repository.SeminarById(seminarId),
                Roles = _roleManager.Roles
            };
            return View( editModel);
        }

        [HttpPost]  
        public async Task<IActionResult> Edit(EditSeminarViewModel model)
        {
            if (model.Seminar.Id != 0 && !ModelState.IsValid) 
                return View(new EditSeminarViewModel { Seminar = model.Seminar , Roles = _roleManager.Roles } );


            var result = await _repository.SaveSeminar(model.Seminar, model.SelectedRoles);
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Create() => 
            View(
                nameof(Edit), 
                new EditSeminarViewModel
                {
                    Seminar = new Seminar(), 
                    Roles = _roleManager.Roles
                });

        [HttpPost]
        public IActionResult Delete(int seminarId)
        {
            var deletedSeminar = _repository.DeleteSeminar(seminarId);
            if (deletedSeminar != null)
                TempData["message"] = $"{deletedSeminar.Name} has been saved";
            return RedirectToAction(nameof(Index));
        }

    }
}
