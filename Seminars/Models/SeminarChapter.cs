using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Seminars.Models
{
    public class SeminarChapter
    {

        public int Id { get; set; }
        public string Name { get; set; }
        [Display(Name = "содержание")]
        public string Content { get; set; }
        public int Order { get; set; }
        public int SeminarId { get; set; }
        public int ParentPartId { get; set; }
        [JsonIgnore]
        [ForeignKey("ParentPartId")]
        public SeminarPart Part { get; set; }
    }
}
