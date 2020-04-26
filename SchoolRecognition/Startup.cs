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



using SchoolRecognition.Services;

using SchoolRecognition.Classes;
using Vereyon.Web;
using Microsoft.Extensions.Logging;
using SchoolRecognition.Models;
using AutoMapper;
using SchoolRecognition.Profiles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

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
            }).AddNewtonsoftJson(setupAction =>
            {
                setupAction.SerializerSettings.ContractResolver =
                new CamelCasePropertyNamesContractResolver();
                setupAction.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore; 
            }).AddXmlDataContractSerializerFormatters();
              

            var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
            services.AddDbContext<SchoolRecognitionContext>(options => options.UseSqlServer(connection));

            var connectionString = new ConnectionString(Configuration.GetConnectionString("SchoolRecognitionConnection"));

            services.AddFlashMessage();

            services.AddSingleton(connectionString);

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new SchoolCategoryProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);


            services.AddControllersWithViews()
                .AddNewtonsoftJson()
                .AddRazorRuntimeCompilation();
          
            //Scoping my  services  
            _ = services.AddTransient<ISchoolCategoryRepository, cSchoolCategoryRepository>();
            _ = services.AddTransient<ISchoolsRepository, cSchoolsRepository>();
            //services.AddTransient<>
            
    
            services.AddMvc()

                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddNToastNotifyToastr();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline. myDapperConnection
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
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
            // other code remove for clarity 
            loggerFactory.AddFile("Logs/mylog-{Date}.txt");
            app.UseRouting();

            app.UseAuthorization();
            app.UseNToastNotify();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
