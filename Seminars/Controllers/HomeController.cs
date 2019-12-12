using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISeminarRepository _seminars;

        public HomeController(ISeminarRepository context) => _seminars = context;
        public IActionResult Index()
        {
            var seminars = _seminars.Seminars;
            return View( seminars );
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