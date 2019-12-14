using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.Models
{
    public class SeminarPartViewModel
    {
        public SeminarPart Part { get; set; }
        public string ParentInputString { get; set; }
    }
}
