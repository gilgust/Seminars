using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Seminars.Models
{
    public class Seminar
    {
        public int Id { get; set; }

        [Display(Name = "название")]
        [Required(ErrorMessage = "пожалуста введите название")]
        public string Name { get; set; }

        [Display(Name = "slug")]
        public string Slug { get; set; }

        [Display(Name = "отрывок")]
        public string Excerpt { get; set; }

        [Display(Name = "содержание")]
        public string Content { get; set; }

        public IEnumerable<SeminarRole> SeminarRoles { get; set; }
        public IEnumerable<SeminarPart> Parts { get; set; }

        public Seminar()
        {
            Parts = new List<SeminarPart>();
            SeminarRoles = new List<SeminarRole>();
        }
    }
}