using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Profiles;
using SchoolRecognition.Services;

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
            }).AddXmlDataContractSerializerFormatters();

            //Add the AutoMapper extension
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
            services.AddDbContext<SchoolRecognitionContext>(options => options.UseSqlServer(connection));

            var connectionString = new ConnectionString(Configuration.GetConnectionString("SchoolRecognitionConnection"));
            services.AddSingleton(connectionString);

            //services.AddTransient<IAccountsRepository, clsAccounts>(provider => new clsAccounts(connectionString));

            _ = services.AddTransient<IRecognitionTypesRepository, cRecognitionTypesRepository>();

           _ = services.AddTransient<IPinsRepository, cPinsRepository>();

            _ = services.AddTransient<ICentresRepository, cCentresRepository>();

            services.AddScoped<IRecognitionTypesRepository, cRecognitionTypesRepository>();
           services.AddScoped<IPinsRepository, cPinsRepository>();

            services.AddControllersWithViews();
            //services.AddControllersWithViews()
            //     .AddNewtonsoftJson()
            //     .AddRazorRuntimeCompilation();

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new PinsProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            

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
                    pattern: "{controller=Payment}/{action=AddSchoolPayment}/{id?}");

                endpoints.MapControllers();
            });
        }
    }
}
