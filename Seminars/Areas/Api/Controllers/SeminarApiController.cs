using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Areas.Api.Controllers
{
    [Route("api/Seminars")]
    // [Authorize(Roles = "admin")]
    [ApiController]
    public class SeminarApiController : ControllerBase
    {
        private readonly ISeminarRepository _repository;

        public SeminarApiController(ISeminarRepository repository) => _repository = repository;

        //GET: api/Seminars
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seminar>>> GetSeminars() => await _repository.GetSeminarsAsync();

        //GET: api/Seminars/1
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Seminar>> GetSeminarById(int id)
        {
            var seminar = await _repository.GetSeminarByIdAsync(id);
            if (seminar == null)
                return NotFound();

            
            return Ok(seminar);
        }

        //PUT: api/Seminars/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Seminar seminar)
        {
            if (id != seminar.Id) return BadRequest();

            var result = await _repository.EditSeminar(seminar);

            if (result == null) return NotFound();

            return Ok(result);
        }

        //POST: api/Seminars
        [HttpPost]
        public async Task<ActionResult<Seminar>> Post(Seminar seminar)
        {
            if (seminar.Id != 0 ) return BadRequest(seminar);

            return Ok(await _repository.AddSeminar(seminar));
        }

        //DELETE: api/Seminars/1
        [HttpDelete("{id}")]
        public async Task<ActionResult<Seminar>> Delete(int id)
        {
            var result = await _repository.DeleteSeminarAsync(id);

            if (result == null) return BadRequest();
         
            return result;
        }
    }
}
