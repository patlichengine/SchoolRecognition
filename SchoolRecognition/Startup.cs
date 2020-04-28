using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using SchoolRecognition.Classes;
using SchoolRecognition.DbContexts;
using SchoolRecognition.Models;
using SchoolRecognition.Services;
using Vereyon.Web;

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
            //use th addController to configure what you want to configure
            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
            })
            //Enable For JSON endpoint data
            //.AddNewtonsoftJson(options =>
            //{
            //    //Fixing JSON Self Referencing Loop Exceptions
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //})
            .AddXmlDataContractSerializerFormatters();

            //Add the AutoMapper extension
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //

            var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
            services.AddDbContext<SchoolRecognitionContext>(options => options.UseSqlServer(connection));

            var connectionString = new ConnectionString(Configuration.GetConnectionString("SchoolRecognitionConnection"));
            services.AddSingleton(connectionString);

            // Add services required for flash message to work.
            services.AddFlashMessage();

            //Application-Layer Services
            //services.AddTransient<IPinsRepository, cPinsRepository>(provider => new cPinsRepository(connectionString));
            //services.AddTransient<IRecognitionTypesRepository, cRecognitionTypesRepository>(provider => new cRecognitionTypesRepository(connectionString));

            services.AddScoped<IAccountsRepository, cAccountsRepository>();
            services.AddScoped<ICentresRepository, cCentresRepository>();
            services.AddScoped<ISchoolPaymentsRepository, cSchoolPaymentsRepository>();
            services.AddScoped<IPinsRepository, cPinsRepository>();
            services.AddScoped<IRecognitionTypesRepository, cRecognitionTypesRepository>();;
            services.AddScoped<ISchoolCategoryRepository, cSchoolCategoryRepository>();
            services.AddScoped<ISchoolsRepository, cSchoolsRepository>();

            services.AddControllersWithViews()
                //Required By FlashMessage 
                .AddNewtonsoftJson(options =>
                {
                    //Fixing JSON Self Referencing Loop Exceptions
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddRazorRuntimeCompilation();
          
            
    
            services.AddMvc()

                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNToastNotifyToastr();

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
            app.UseNToastNotify();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                
                endpoints.MapControllers();
            });
        }
    }
}
