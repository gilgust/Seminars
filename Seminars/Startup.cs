using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Seminars.Areas.Admin.Controllers;
using Seminars.Models;
using Seminars.Repositories;
using Seminars.security;

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

            services.AddIdentity<AppUser, AppRole>(opt =>{
                opt.Password.RequiredLength = 2;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit= false; })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opt =>{
                    opt.RequireHttpsMetadata = false;
                    opt.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,
                        ValidateLifetime = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true
                    };
                });

            services.AddTransient<ISeminarRepository, SeminarRepository>();
            services.AddTransient<ISeminarPartRepository, SeminarPartRepository>();
            services.AddTransient<ISeminarChapterRepository, ChapterRepository>();

            services.AddMvc()
                .AddRazorRuntimeCompilation();

            services.AddSpaStaticFiles(config => { config.RootPath = "ClientApp/build"; });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                app.UseStatusCodePages();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: null,
                    pattern: "Account/{action=Login}",
                    defaults: new { controller = "Account" });
                endpoints.MapControllerRoute(
                        name: null,
                        pattern: "{area=Admin}/Role/{action=Index}",
                        defaults: new { controller = "RoleAdmin"});
                    endpoints.MapControllerRoute(
                        name: "areas",
                        pattern: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");
                    endpoints.MapRazorPages();
                });
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                    spa.UseReactDevelopmentServer(npmScript: "start");
                
            });
        }
    }
}
