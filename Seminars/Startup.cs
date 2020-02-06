using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Seminars.Areas.Admin.Controllers;
using Seminars.Models;
using Seminars.Repositories;

namespace Seminars
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<AppDbContext>( options =>
                  // options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
                  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<AppUser, IdentityRole>(opt =>{
                opt.Password.RequiredLength = 2;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit= false;
            }).AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<ISeminarRepository, SeminarRepository>();
            services.AddTransient<ISeminarPartRepository, SeminarPartRepository>();
            services.AddTransient<ISeminarChapterRepository, ChapterRepository>();

            services.AddMvc();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting(); // используем систему маршрутизации
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(
                        name: null,
                        pattern: "{area=Admin}/Role/{action=Index}",
                        defaults: new { controller = "RoleAdmin"});
                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(
                        name: null,
                        pattern: "{controller=Home}/{action=Index}/{id?}");
                });

            // AppDbContext.CreateAdminAccount(app.ApplicationServices, Configuration).Wait();
        }
    }
}
