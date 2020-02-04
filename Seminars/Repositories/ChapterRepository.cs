using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seminars.Models;

namespace Seminars.Repositories
{
    public class ChapterRepository : ISeminarChapterRepository
    {
        protected AppDbContext _context;

        public ChapterRepository(AppDbContext context) => _context = context;

        public IQueryable<SeminarChapter> Chapters => _context.SeminarChapter;
        public async Task<List<SeminarChapter>> GetChaptersAsync() => await Chapters.ToListAsync();

        public async Task<SeminarChapter> GetChapterByIdAsync(int id) => await _context.SeminarChapter.FindAsync(id);
        
        public async Task<SeminarChapter> EditChapterAsync(SeminarChapter chapter)
        {
            _context.Entry(chapter).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeminarChapterExists(chapter.Id))
                    return null;
                else
                    throw;
            }

            return chapter;
        }

        public async Task<SeminarChapter> AddChapterAsync(SeminarChapter chapter)
        {
            await _context.SeminarChapter.AddAsync(chapter);
            await _context.SaveChangesAsync();

            return chapter;
        }

        public async Task<SeminarChapter> DeleteChapterAsync(int id)
        {

            var seminarChapter = await _context.SeminarChapter.FindAsync(id);

            if (seminarChapter == null)
                return null;

            _context.SeminarChapter.Remove(seminarChapter);
            await _context.SaveChangesAsync();

            return seminarChapter;
        }
        private bool SeminarChapterExists(int id) => _context.SeminarChapter.Any(e => e.Id == id);
    }
}
