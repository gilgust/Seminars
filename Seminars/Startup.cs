using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using Seminars.Repositories;

namespace Seminars
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //    "DefaultConnection1": "Server=(localdb)\\MSSQLLocalDB;Database=sqlSerwerAppSeminarsTest;AttachDbFilename=D:\\databases\\sqlSerwerAppSeminarsTest.mdf;Trusted_Connection=True;MultipleActiveResultSets=true"
            services.AddDbContextPool<AppDbContext>( options => 
                options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));

            services.AddTransient<ISeminarRepository, SeminarRepository>();

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();

            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(name: "areas", template: "{area:exists}/{controller=HomeAdmin}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: null, 
                    template: "{controller=Home}/{action=Index}/{id?}"
                    );
                routes.MapRoute( name: null, template: "{controller=Home}/{action=Index}/{id?}");
                });
        }
    }
}
