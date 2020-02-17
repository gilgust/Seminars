using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seminars.Models;

namespace Seminars.Repositories
{
    public interface ISeminarRepository
    {
        IQueryable<Seminar> Seminars { get; }
        void SaveSeminar(Seminar seminar);
        Task<string> SaveSeminar(Seminar seminar, string[] selectedRoles);
        Seminar DeleteSeminar(int seminarId);
        Seminar SeminarBySlug(string slug);
        Seminar SeminarById(int id);

        Task<List<Seminar>> GetSeminarsAsync();
        Task<Seminar> GetSeminarByIdAsync(int id);
        Task<Seminar> EditSeminar(Seminar seminar);
        Task<Seminar> AddSeminar(Seminar seminar);
        Task<Seminar> DeleteSeminarAsync(int seminarId);
    }
}
