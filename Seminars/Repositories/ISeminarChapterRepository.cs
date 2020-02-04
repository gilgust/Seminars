using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seminars.Models;

namespace Seminars.Repositories
{
    public interface ISeminarChapterRepository
    {
        IQueryable<SeminarChapter> Chapters { get; }

        Task<List<SeminarChapter>> GetChaptersAsync();
        Task<SeminarChapter> GetChapterByIdAsync(int id);
        Task<SeminarChapter> EditChapterAsync(SeminarChapter chapter);
        Task<SeminarChapter> AddChapterAsync(SeminarChapter chapter);
        Task<SeminarChapter> DeleteChapterAsync(int id);
    }
}
