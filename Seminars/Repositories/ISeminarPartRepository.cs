using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seminars.Models;

namespace Seminars.Repositories
{
    public interface ISeminarPartRepository
    {
        IQueryable<SeminarPart> SeminarParts { get; }
        void SaveSeminarPart(SeminarPart part);
        SeminarPart DeleteSeminarPart(int partId);
        SeminarPart PartById(int id);

        Task<List<SeminarPart>> GetPartsAsync();
        Task<SeminarPart> GetPartByIdAsync(int id);
        Task<SeminarPart> EditPartAsync(SeminarPart part);
        Task<SeminarPart> AddPartAsync(SeminarPart part);
        Task<SeminarPart> DeleteSeminarPartAsync(int partId);
    }
}
