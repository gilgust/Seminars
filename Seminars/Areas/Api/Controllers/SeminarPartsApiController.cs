using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Areas.Api.Controllers
{
    [Route("api/Parts")]
    [ApiController]
    public class SeminarPartsApiController : ControllerBase
    {
        private readonly ISeminarPartRepository _context;

        public SeminarPartsApiController(ISeminarPartRepository context) => _context = context;

        // GET: api/SeminarParts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeminarPart>>> GetSeminarParts() => await _context.SeminarParts.ToListAsync();
        

        //GET: api/SeminarParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeminarPart>> GetSeminarPart(int id)
        {
            var seminarPart = await _context.GetPartByIdAsync(id);

            if (seminarPart == null)
                return NotFound();

            return seminarPart;
        }


        // PUT: api/SeminarParts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeminarPart(int id,  [FromBody] SeminarPart seminarPart)
        {
            if (seminarPart.Id != id)
                return BadRequest();

            var result = await _context.EditPartAsync(seminarPart);

            return Ok(result);
        }

        // POST: api/SeminarParts
        [HttpPost]
        public async Task<ActionResult<SeminarPart>> PostSeminarPart(SeminarPart seminarPart)
        {
            await _context.AddPartAsync(seminarPart);

            return CreatedAtAction("GetSeminarPart", new { id = seminarPart.Id }, seminarPart);
        }


        // DELETE: api/SeminarParts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeminarPart>> DeleteSeminarPart(int id)
        {
            var seminarPart = await _context.DeleteSeminarPartAsync(id);

            if (seminarPart == null)
                return NotFound();
            
            return Ok(seminarPart);
        }
    }
}