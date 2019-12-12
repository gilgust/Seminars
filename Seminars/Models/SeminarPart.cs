using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.Models
{
    public class SeminarPart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SeminarId { get; set; }
        public int ParentPartId { get; set; }
        public Seminar Seminar { get; set; }
        [ForeignKey("ParentPartId")]
        public SeminarPart Part { get; set; }
        public IEnumerable<SeminarPart> Parts { get; set; }

    }
}
