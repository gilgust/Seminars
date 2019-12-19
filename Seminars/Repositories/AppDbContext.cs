using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Seminars.Models;

namespace Seminars.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) :base(options){}

        public virtual DbSet<Seminar> Seminars { get; set; }
        public virtual DbSet<SeminarPart> SeminarParts { get; set; }
        public DbSet<FileModel> Files{ get; set; }
        public DbSet<Seminars.Models.SeminarChapter> SeminarChapter { get; set; }
    }
}