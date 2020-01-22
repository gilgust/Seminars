using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
                Data = GetData(nameof(Index))
            });
        }

        public IActionResult OtherAction() => View("Index", new HomeIndexViewModel()
        {
            Seminars = _seminars.Seminars.ToList(),
            Data = GetData(nameof(OtherAction))
        });

        [Authorize]
        public IActionResult OnlyAuthorizeAction() => View("Index", new HomeIndexViewModel()
        {
            Seminars = _seminars.Seminars.ToList(),
            Data = GetData(nameof(OnlyAuthorizeAction))
        });
        [Authorize(Roles = "User")]
        public IActionResult OnlyUserAction() => View("Index", new HomeIndexViewModel()
        {
            Seminars = _seminars.Seminars.ToList(),
            Data = GetData(nameof(OnlyUserAction))
        });

        public IActionResult Seminar(string slug)
        {
            var seminar = _seminars.SeminarBySlug(slug);
            if (seminar == null)
                return NotFound();
            
            return View(seminar);
        }
        [Route("/TestSeminarApi")]
        public ViewResult TestSeminarApi() =>  View(_seminars.Seminars.ToList());
        

        private Dictionary<string, object> GetData(string actionName) => 
        new Dictionary<string, object>
        {
            ["Action"] = actionName,
            ["User"] = HttpContext.User.Identity.Name,
            ["Authenticated"] = HttpContext.User.Identity.IsAuthenticated,
            ["Auth Type"] = HttpContext.User.Identity.AuthenticationType,
            ["In User Role"] = HttpContext.User.IsInRole("User")
        };
    }
}