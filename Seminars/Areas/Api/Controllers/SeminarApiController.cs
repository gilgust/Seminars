using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Areas.Api.Controllers
{
    [Route("api/Seminars")]
    [ApiController]
    public class SeminarApiController : ControllerBase
    {
        private readonly ISeminarRepository _repository;

        public SeminarApiController(ISeminarRepository repository) => _repository = repository;

        //GET: api/Seminars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Seminar>>> GetSeminars() => await _repository.GetSeminarsAsync();

        //GET: api/Seminars/1
        [HttpGet("{id}")]
        public async Task<ActionResult<Seminar>> GetSeminarById(int id)
        {
            var seminar = await _repository.GetSeminarByIdAsync(id);
            if (seminar == null)
                return NotFound();

            
            return Ok(JsonConvert.SerializeObject(seminar));
        }

        //GET: api/Seminars/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Seminar seminar)
        {
            if (id != seminar.Id) return BadRequest();

            var result = await _repository.EditSeminar(seminar);

            if (result == null) return NotFound();

            return Ok(result);
        }

        //POST: api/Seminars/1
        [HttpPost]
        public async Task<ActionResult<Seminar>> Post(Seminar seminar)
        {
            if (seminar.Id != 0 ) return BadRequest(seminar);

            return Ok(await _repository.AddSeminar(seminar));
        }

        //POST: api/Seminars/1
        [HttpDelete]
        public async Task<ActionResult<Seminar>> Delete(int seminarId)
        {
            var result = await _repository.DeleteSeminarAsync(seminarId);

            if (result == null) return BadRequest();
         
            return result;
        }
    }
}
