using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seminars.Models
{
    public class AppRole : IdentityRole
    {
        public AppRole() { }
        public AppRole(string roleName) : base(roleName){}

        public IEnumerable<SeminarRole> SeminarRoles { get; set; }
    }

    public class SeminarRole
    {
        public int SeminarId { get; set; }
        public Seminar Seminar { get; set; }
        public string RoleId { get; set; }
        public AppRole Role { get; set; }
    }
}
