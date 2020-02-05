using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Seminars.Models
{
    public class SeminarPart
    {
        public SeminarPart() => Chapters = new List<SeminarChapter>();

        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "содержание")]
        public string Content { get; set; }
        public int Order { get; set; }
        public int SeminarId { get; set; }
        [JsonIgnore]
        public Seminar Seminar { get; set; }
        public IEnumerable<SeminarChapter> Chapters{ get; set; }
    }
}
