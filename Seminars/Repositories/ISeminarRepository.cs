using System;
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
        Seminar DeleteSeminar(int seminarId);
        Seminar SeminarBySlug(string slug);
        Seminar SeminarById(int id);
        string AvailableSlug(int seminarId, string slug);

    }
}
