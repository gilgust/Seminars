using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars.Areas.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeminarPartsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeminarPartsController(AppDbContext context) => _context = context;

        // GET: api/SeminarParts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeminarPart>>> GetSeminarParts() => await _context.SeminarParts.ToListAsync();
        

        //GET: api/SeminarParts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeminarPart>> GetSeminarPart(int id)
        {
            var seminarPart = await _context.SeminarParts.FindAsync(id);

            if (seminarPart == null)
                return NotFound();

            return seminarPart;
        }


        // PUT: api/SeminarParts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeminarPart(int id, SeminarPart seminarPart)
        {
            if (id != seminarPart.Id)
                return BadRequest();

            _context.Entry(seminarPart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeminarPartExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // POST: api/SeminarParts
        [HttpPost]
        public async Task<ActionResult<SeminarPart>> PostSeminarPart(SeminarPart seminarPart)
        {
            _context.SeminarParts.Add(seminarPart);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeminarPart", new { id = seminarPart.Id }, seminarPart);
        }


        // DELETE: api/SeminarParts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeminarPart>> DeleteSeminarPart(int id)
        {
            var seminarPart = await _context.SeminarParts.FindAsync(id);
            if (seminarPart == null)
                return NotFound();

            _context.SeminarParts.Remove(seminarPart);
            await _context.SaveChangesAsync();

            return seminarPart;
        }

        private bool SeminarPartExists(int id) => _context.SeminarParts.Any(e => e.Id == id);
    }
}