using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolRecognition.Context;
using SchoolRecognition.Helpers;
using SchoolRecognition.Models;
using SchoolRecognition.Repository;
using SchoolRecognition.Services;
using SchoolRecognition.Data;

namespace SchoolRecognition
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //custom Henry Connection
            services.AddDbContext<SchoolRecognitionContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection")));

            //custom project Connection String
            //var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
            //services.AddDbContext<SchoolRecognitionContext>(options => options.UseSqlServer(connection));




            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation();
            //Scoping my  services  
            _ = services.AddTransient<SchoolCategoriesRepo, SchoolCategoriesService>();
            //services.AddTransient<>



            //Register dapper in scope  
            services.AddScoped<IDapperHelper, DapperHelper>();

            //My Scaffold App
            //services.AddDbContext<SchoolRecognitionAppContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("SchoolRecognitionAppContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. myDapperConnection
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
