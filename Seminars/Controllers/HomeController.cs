using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.Repositories;
using Seminars.ViewModel;

namespace Seminars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeminarRepository _seminars;

        public HomeController(ISeminarRepository context) => _seminars = context;
        public IActionResult Index()
        {

            return View(new HomeIndexViewModel(){
                Seminars = _seminars.Seminars.ToList(),
                Data = new Dictionary<string, object>
                {
                    ["Placeholder"] = "Placeholder"
                }
            });
        }
        

        public IActionResult Seminar(string slug)
        {
            var seminar = _seminars.SeminarBySlug(slug);
            if (seminar == null)
                return NotFound();
            
            return View(seminar);
        }
    }
}