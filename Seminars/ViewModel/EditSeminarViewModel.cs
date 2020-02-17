using Seminars.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.ViewModel
{
    public class EditSeminarViewModel
    {
        public Seminar Seminar { get; set; }
        public string[] SelectedRoles { get; set; }
        public IEnumerable<AppRole> Roles { get; set; }
    }
}
