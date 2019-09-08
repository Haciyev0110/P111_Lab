using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Academic.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Academic.Models;
using Microsoft.AspNetCore.Identity;

namespace Academic
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration configuration)
        {
            _config = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_config["ConnectionStrings:DefaultConnection"]);
            });

            services.AddIdentity<User, IdentityRole>(m =>
             {
                 m.Password.RequireUppercase = false;
                 m.Password.RequireNonAlphanumeric = false;
                 m.Password.RequireLowercase = false;
                 m.Password.RequireDigit = false;
                 m.Lockout.MaxFailedAccessAttempts = 5;
             })
                 .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<AppDbContext>();
            services.ConfigureApplicationCookie(option =>
            {

                option.LoginPath = new PathString("/AcademicArea/Account/Login");
                option.AccessDeniedPath = new PathString("/AcademicArea/Account/Login");

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "areas",
                   template: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                routes.MapRoute(
                    "default",
                    "{controller=home}/{action=index}/{id?}"
                    );
            });
           
        }
    }
}
