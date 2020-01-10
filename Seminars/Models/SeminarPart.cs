using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Seminars.Models
{
    public class SeminarPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "содержание")]
        public string Content { get; set; }
        public int Order { get; set; }
        public int SeminarId { get; set; }
        public Seminar Seminar { get; set; }
        public IEnumerable<SeminarChapter> Chapters{ get; set; }

        public SeminarPart() => Chapters = new List<SeminarChapter>();
    }
}
