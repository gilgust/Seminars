using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Seminars.Models;

namespace Seminars.ViewModel
{
    public class EditSeminarModel
    {
        public Seminar Seminar { get; set; }
        public IEnumerable<IdentityRole> ForRoles { get; set; }
    }
}
