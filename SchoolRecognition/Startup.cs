using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
            ////use th addController to configure what you want to configure
            //services.AddControllers(setupAction =>
            //{
            //    setupAction.ReturnHttpNotAcceptable = true;
            //})
            ////Enable For JSON endpoint data
            //.AddNewtonsoftJson(options =>
            //{
            //    //Fixing JSON Self Referencing Loop Exceptions
            //    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            //})
            //.AddXmlDataContractSerializerFormatters();
            ////Caching
            //services.AddResponseCaching();

            services.AddControllers(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                setupAction.CacheProfiles.Add("240SecondsCacheProfile",
                    new CacheProfile()
                    {
                        Duration = 240
                    });
            })

                .AddNewtonsoftJson(setupAction =>
                {
                    setupAction.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                    //setupAction.SerializerSettings.ContractResolver = new DefaultContractResolver();
                })
                .AddXmlDataContractSerializerFormatters()
                .ConfigureApiBehaviorOptions(setupAction =>
                {
                    setupAction.InvalidModelStateResponseFactory = context =>
                    {
                        var problemDetailsFactory = context.HttpContext.RequestServices
                        .GetRequiredService<ProblemDetailsFactory>();
                        var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
                                context.HttpContext,
                                context.ModelState
                            );

                        //add additional infor not added by default
                        problemDetails.Detail = "See the error fields for details.";
                        problemDetails.Instance = context.HttpContext.Request.Path;

                        //fill out which status code to use
                        var actionExecutionContext =
                        context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                        //if there are modelstate error & all argument were correct
                        //found/parsed we 're dealing with validation errors
                        if ((context.ModelState.ErrorCount > 0) &&
                        (actionExecutionContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
                        {
                            problemDetails.Type = "http://recognition.waec.org.ng/modelvalidationproblem";
                            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
                            problemDetails.Title = "One or more validation error occured";

                            return new UnprocessableEntityObjectResult(problemDetails)
                            {
                                ContentTypes = { "application/problem+json" }
                            };
                        }

                        //if one of the arguement was not correctly found/couldn't be found
                        //we are dealing with null/unparsable input
                        problemDetails.Status = StatusCodes.Status400BadRequest;
                        problemDetails.Title = "One or more error on input occured.";
                        return new BadRequestObjectResult(problemDetails)
                        {
                            ContentTypes = { "application/problem+json" }
                        };
                    };
                });
            services.Configure<MvcOptions>(config =>
            {
                var newtonsoftJsonOutputFormatter = config.OutputFormatters
                .OfType<NewtonsoftJsonOutputFormatter>()?.FirstOrDefault();

                if (newtonsoftJsonOutputFormatter != null)
                {
                    newtonsoftJsonOutputFormatter.
                    SupportedMediaTypes.Add("application/vnd.waec.hateoas+json");
                }
            });

            //Add the AutoMapper extension
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //

            var connection = Configuration.GetConnectionString("SchoolRecognitionConnection");
            //install the Microsoft.EntityFrameworkCore.Proxies to enable lazy-loading
            services.AddDbContext<SchoolRecognitionContext>
                (options =>
                options
                //Enabling lazy-loading
                .UseLazyLoadingProxies()
                .UseSqlServer(connection)
                );

            var connectionString = new ConnectionString(Configuration.GetConnectionString("SchoolRecognitionConnection"));

            //set the Email connections
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSender, EmailSender>();

            services.AddSingleton(connectionString);

            // Add services required for flash message to work.
            services.AddFlashMessage();

            //Application-Layer Services
            //services.AddTransient<IPinsRepository, cPinsRepository>(provider => new cPinsRepository(connectionString));
            //services.AddTransient<IRecognitionTypesRepository, cRecognitionTypesRepository>(provider => new cRecognitionTypesRepository(connectionString));


            services.AddScoped<IAccountsRepository, cAccountsRepository>();
            services.AddScoped<IApplicationSettingsRepository, cApplicationSettingsRepository>();
            services.AddScoped<ICentresRepository, cCentresRepository>();
            services.AddScoped<ILocalGovernmentsRepository, cLocalGovernmentsRepository>();
            services.AddScoped<IOfficesRepository, cOfficesRepository>();
            services.AddScoped<IOfficeLocalGovernmentsRepository, cOfficeLocalGovernmentsRepository>();
            services.AddScoped<IOfficeStatesRepository, cOfficeStatesRepository>();
            services.AddScoped<IOfficeTypesRepository, cOfficeTypesRepository>();
            services.AddScoped<IPinsRepository, cPinsRepository>();
            services.AddScoped<IRecognitionTypesRepository, cRecognitionTypesRepository>();
            services.AddScoped<ISchoolCategoryRepository, cSchoolCategoryRepository>();
            services.AddScoped<ISchoolsRepository, cSchoolsRepository>();
            services.AddScoped<ISchoolPaymentsRepository, cSchoolPaymentsRepository>();
            services.AddScoped<IStatesRepository, cStatesRepository>();
            services.AddScoped<ISubjectsRepository, cSubjectsRepository>();



            // register PropertyMappingService
            services.AddTransient<IPropertyMappingService, PropertyMappingService>();

            // register PropertyCheckerService
            services.AddTransient<IPropertyCheckerService, PropertyCheckerService>();

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

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
                options.SuppressXFrameOptionsHeader = false;
            });

            services.AddHttpContextAccessor();
            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(20);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

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
            //Enable caching
            app.UseResponseCaching();
            //
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
           

            app.UseAuthorization();
            app.UseNToastNotify();
            app.UseSession();
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
