using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.Repositories;
using Seminars.infrastructure;

namespace Seminars.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeminarAdminController : Controller
    {
        private readonly ISeminarRepository _repository;

        public SeminarAdminController(ISeminarRepository repository) => _repository = repository;
        public ViewResult Index() => View(_repository.Seminars.ToList());
        

        public ViewResult Edit(int seminarId)
        {
            return View(_repository.Seminars.FirstOrDefault(s => s.Id == seminarId));
        }

        [HttpPost]
        public IActionResult Edit(Seminar seminar)
        {
            if (seminar.Id != 0 && !ModelState.IsValid) return View(seminar);

            if (string.IsNullOrWhiteSpace(seminar.Slug))
            {
                var trn = new TranslitMethods.Translitter();
                var translatedName = trn.Translit(seminar.Name, TranslitMethods.TranslitType.Iso);

                var buffSlug = WebUtility.UrlEncode(translatedName)?.Replace('+', '-')??"no-name-seminar";
                seminar.Slug = _repository.AvailableSlug( seminar.Id, buffSlug);
            }

            _repository.SaveSeminar(seminar);
            TempData["message"] = $"{seminar.Name} has been saved";
            return RedirectToAction(nameof(Index));
        }

        public ViewResult Create() => View(nameof(Edit), new Seminar());

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
