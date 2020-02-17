using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Seminars.Models;

namespace Seminars.Repositories
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext( DbContextOptions<AppDbContext> options) :base(options){}

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<AppRole>>();

            var userName = configuration["Data:AdminUser:Name"];
            var email = configuration["Data:AdminUser:Email"];
            var password = configuration["Data:AdminUser:Password"];
            var role = configuration["Data:AdminUser:Role"];

            if (await userManager.FindByNameAsync(userName) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                    await roleManager.CreateAsync(new AppRole(role));

                var user = new AppUser {
                    UserName = userName,
                    Email = email
                };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(user, role);
                
            }
        }

        public virtual DbSet<Seminar> Seminars { get; set; }
        public virtual DbSet<SeminarPart> SeminarParts { get; set; }
        public DbSet<FileModel> Files{ get; set; }
        public DbSet<SeminarChapter> SeminarChapter { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<SeminarRole> SeminarRoles { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<SeminarRole>().HasKey(sr => new { sr.RoleId, sr.SeminarId });

            modelBuilder.Entity<SeminarRole>()
                .HasOne(sr => sr.Role)
                .WithMany(sr => sr.SeminarRoles)
                .HasForeignKey(sr => sr.RoleId);


            modelBuilder.Entity<SeminarRole>()
                .HasOne(sr => sr.Seminar)
                .WithMany(sr => sr.SeminarRoles)
                .HasForeignKey(sc => sc.SeminarId);
        }
    }
}