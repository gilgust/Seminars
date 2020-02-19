using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Seminars.Areas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestTokenController : Controller
    {
        [Authorize]
        [Route("getlogin")]
        public IActionResult GetLogin() => Ok($"Ваш логин: {User.Identity.Name}");

        [Authorize(Roles = "admin")]
        [Route("getRole")]
        public IActionResult GetRole() => Ok("Ваша роль: admin");
    }
}
