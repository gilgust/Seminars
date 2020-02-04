using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seminars.Models;

namespace Seminars.Repositories
{
    public class SeminarPartRepository : ISeminarPartRepository
    {
        private readonly AppDbContext _context;

        public SeminarPartRepository(AppDbContext context) => _context = context;
        public IQueryable<SeminarPart> SeminarParts => _context.SeminarParts;
        public void SaveSeminarPart(SeminarPart part)
        {
            if (part.Id == 0)
                _context.SeminarParts.Add(part);
            else
                _context.Entry(part).State = EntityState.Modified;

            _context.SaveChanges();

        }

        public SeminarPart DeleteSeminarPart(int partId)
        {
            var dbEntity = _context.SeminarParts.FirstOrDefault(p => p.Id == partId);

            if (dbEntity == null) return null;

            _context.SeminarParts.Remove(dbEntity);
            _context.SaveChanges();
            return dbEntity;
        }

        public SeminarPart PartById(int id) => 
            _context.SeminarParts
                .Where(p=> p.Id == id)
                .Include(p => p.Chapters)
                .FirstOrDefault();


        public async Task<List<SeminarPart>> GetPartsAsync() => await _context.SeminarParts.ToListAsync();

        public async Task<SeminarPart> GetPartByIdAsync(int id)
        {
            var seminar = await _context.SeminarParts.FindAsync(id);

            return seminar == null ? null : PartById(id);
        }

        public async Task<SeminarPart> EditPartAsync(SeminarPart part)
        {
            _context.Entry(part).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartExist(part.Id))
                    return null;
                else
                    throw;
            }

            return part;
        }

        public async Task<SeminarPart> AddPartAsync(SeminarPart part)
        {
            _context.SeminarParts.Add(part);
            await _context.SaveChangesAsync();

            return part;
        }

        public async Task<SeminarPart> DeleteSeminarPartAsync(int partId)
        {
            var part = await _context.SeminarParts.FirstOrDefaultAsync(s => s.Id == partId);

            if (part == null) return null;

            _context.SeminarParts.Remove(part);
            await _context.SaveChangesAsync();

            return part;
        }

        private bool PartExist(int partId) => _context.SeminarParts.Any(p => p.Id == partId);
    }
}
