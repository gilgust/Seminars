using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
using Seminars.infrastructure;
using Seminars.Models;

namespace Seminars.Repositories
{
    public class SeminarRepository : ISeminarRepository
    {
        private readonly AppDbContext _context;

        public SeminarRepository(AppDbContext ctx) => _context = ctx;
        public IQueryable<Seminar> Seminars => _context.Seminars;

        public void SaveSeminar(Seminar seminar)
        {
            //generate slug if isn't exist
            if (string.IsNullOrWhiteSpace(seminar.Slug))
            {
                var trn = new TranslitMethods.Translitter();
                var translatedName = trn.Translit(seminar.Name, TranslitMethods.TranslitType.Iso);

                var buffSlug = WebUtility.UrlEncode(translatedName)?.Replace('+', '-') ?? "no-name-seminar";
                seminar.Slug = AvailableSlug(seminar.Id, buffSlug);
            }

            if (seminar.Id == 0)
                _context.Seminars.Add(seminar);
            else
            {
                var dbEntity = _context.Seminars.FirstOrDefault(s => s.Id == seminar.Id);
                if (dbEntity != null)
                {
                    dbEntity.Name = seminar.Name;
                    dbEntity.Content = seminar.Content;
                    dbEntity.Slug = seminar.Slug;
                    dbEntity.Excerpt = seminar.Excerpt;
                }
            }
            _context.SaveChanges();
        }

        public Seminar DeleteSeminar(int seminarId)
        {
            var dbEntity = _context.Seminars.FirstOrDefault(s => s.Id == seminarId);
            if (dbEntity != null)
            {
                _context.Seminars.Remove(dbEntity);
                _context.SaveChanges();
            }

            return dbEntity;
        }


        public Seminar SeminarBySlug(string slug) =>
            _context.Seminars
                .Where(s => s.Slug == slug)
                .Include(s => s.Parts)
                .ThenInclude(p => p.Chapters)
                .FirstOrDefault();

        public Seminar SeminarById(int id) =>
            _context.Seminars
                .Where(s => s.Id == id)
                .Include(s => s.Parts)
                .ThenInclude(p => p.Chapters)
                .FirstOrDefault(s => s.Id == id);

      

        public async Task<List<Seminar>> GetSeminarsAsync() => await _context.Seminars.ToListAsync();

        public async Task<Seminar> GetSeminarByIdAsync(int id)
        {
            var seminar = await _context.Seminars.FindAsync(id);

            return seminar == null ? null : SeminarById(id);
        }

        public async Task<Seminar> EditSeminar(Seminar seminar)
        {
            _context.Entry(seminar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SeminarExists(seminar.Id))
                    return null;
                else
                    throw;
            }

            return seminar;
        }

        public async Task<Seminar> AddSeminar(Seminar seminar)
        {
            //generate slug if isn't exist
            if (string.IsNullOrWhiteSpace(seminar.Slug))
            {
                var trn = new TranslitMethods.Translitter();
                var translatedName = trn.Translit(seminar.Name, TranslitMethods.TranslitType.Iso);

                var buffSlug = WebUtility.UrlEncode(translatedName)?.Replace('+', '-') ?? "no-name-seminar";
                seminar.Slug = AvailableSlug(seminar.Id, buffSlug);
            }



            _context.Seminars.Add(seminar);
            await _context.SaveChangesAsync();

            return seminar;
        }

        public async Task<Seminar> DeleteSeminarAsync(int seminarId)
        {
            var seminar = await _context.Seminars.FirstOrDefaultAsync(s => s.Id == seminarId);

            if (seminar == null) return null;
            
            _context.Seminars.Remove(seminar);
            await _context.SaveChangesAsync();

            return seminar;
        }


        private bool SeminarExists(int id) => _context.Seminars.Any(e => e.Id == id);
        private string AvailableSlug(int seminarId, string slug)
        {
            var counter = 0;
            var bufSlug = slug;
            while (true)
            {
                var dbEntity = _context.Seminars.FirstOrDefault(s => s.Slug == bufSlug);

                if (dbEntity == null || dbEntity.Id == seminarId)
                    return bufSlug;

                bufSlug = slug + "-" + counter++;
            }
        }
    }
}
