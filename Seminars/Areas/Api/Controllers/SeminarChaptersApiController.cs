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
    public class SeminarChaptersApiController : ControllerBase
    {
        private readonly ISeminarChapterRepository _context;

        public SeminarChaptersApiController(ISeminarChapterRepository context) => _context = context;

        // GET: api/SeminarChapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SeminarChapter>>> GetSeminarChapter() => await _context.GetChaptersAsync();
        

        // GET: api/SeminarChapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SeminarChapter>> GetSeminarChapter(int id)
        {
            var seminarChapter = await _context.GetChapterByIdAsync(id);

            if (seminarChapter == null)
                return NotFound();

            return Ok(seminarChapter);
        }

        // PUT: api/SeminarChapters/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSeminarChapter(int id, SeminarChapter seminarChapter)
        {
            if (id != seminarChapter.Id)
                return BadRequest();

            var result = await _context.EditChapterAsync(seminarChapter);

            if (result == null)
                return NotFound();

            return Ok(seminarChapter);
        }

        // POST: api/SeminarChapters
        [HttpPost]
        public async Task<ActionResult<SeminarChapter>> PostSeminarChapter(SeminarChapter seminarChapter)
        {
            var result = await _context.AddChapterAsync(seminarChapter);

            return CreatedAtAction("GetSeminarChapter", new { id = result.Id }, result);
        }

        // DELETE: api/SeminarChapters/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SeminarChapter>> DeleteSeminarChapter(int id)
        {
            var result = await _context.DeleteChapterAsync(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
