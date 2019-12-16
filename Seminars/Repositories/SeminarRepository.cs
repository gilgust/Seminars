using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.EntityFrameworkCore;
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
            if (seminar.Id == 0)
            {
                _context.Seminars.Add(seminar);
            }
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

        public string AvailableSlug(int seminarId, string slug)
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

        public Seminar SeminarBySlug(string slug) 
        {
            var dbEntity = _context.Seminars
                .Include(s => s.Parts)
                .FirstOrDefault(s => s.Slug == slug);

            dbEntity.Parts = SeminarPartsToTree(dbEntity.Parts);

            return dbEntity;
        }
            
        public Seminar SeminarById(int id)
        {
            var dbEntity = _context.Seminars
                .Include(s => s.Parts)
                .FirstOrDefault(s => s.Id == id);

            dbEntity.Parts = SeminarPartsToTree(dbEntity.Parts);

            return dbEntity;
        }

        private List<SeminarPart> SeminarPartsToTree(IEnumerable<SeminarPart> parts, int parantPartId = 0)
        {
            var result = parts.Where(p => p.ParentPartId == parantPartId);

            if (!parts.Any(p => p.ParentPartId == parantPartId))
                return null;

            foreach (var part in parts)
            {
                part.Parts = SeminarPartsToTree(parts, part.ParentPartId);
            }

            return result.ToList();
        }
    }
}
