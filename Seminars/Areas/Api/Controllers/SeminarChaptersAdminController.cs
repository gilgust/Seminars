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
    [Route("api/Chapters")]
    [ApiController]
    public class SeminarChaptersAdminController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SeminarChaptersAdminController(AppDbContext context) => _context = context;

        // GET: api/SeminarChapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeminarChapter>>> GetSeminarChapter() => await _context.SeminarChapter.ToListAsync();
        

        // GET: api/SeminarChapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeminarChapter>> GetSeminarChapter(int id)
        {
            var seminarChapter = await _context.SeminarChapter.FindAsync(id);

            if (seminarChapter == null)
            {
                return NotFound();
            }

            return seminarChapter;
        }

        // PUT: api/SeminarChapters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeminarChapter(int id, SeminarChapter seminarChapter)
        {
            if (id != seminarChapter.Id)
                return BadRequest();

            _context.Entry(seminarChapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeminarChapterExists(id))
                    return NotFound();
                else
                    throw;
            }

            return Ok(seminarChapter);
        }

        // POST: api/SeminarChapters
        [HttpPost]
        public async Task<ActionResult<SeminarChapter>> PostSeminarChapter(SeminarChapter seminarChapter)
        {
            _context.SeminarChapter.Add(seminarChapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSeminarChapter", new { id = seminarChapter.Id }, seminarChapter);
        }

        // DELETE: api/SeminarChapters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeminarChapter>> DeleteSeminarChapter(int id)
        {
            var seminarChapter = await _context.SeminarChapter.FindAsync(id);
            if (seminarChapter == null)
                return NotFound();
            

            _context.SeminarChapter.Remove(seminarChapter);
            await _context.SaveChangesAsync();

            return seminarChapter;
        }

        private bool SeminarChapterExists(int id) => _context.SeminarChapter.Any(e => e.Id == id);
        
    }
}
