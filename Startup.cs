using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using WebApi.Helpers;
using WebApi.Services;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Cors;


using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;




using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;


namespace WebApi
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
             services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        }));
    services.AddDbContext<DataContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=Auth_test;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<LopezContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Lopez;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<ScheduleContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=Auth_test;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<HKMasterContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Master;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<GovelonowContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=LoginAppNewDatabase;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<FastCareContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_FastCare_HealthKinect2;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<ArisContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Aris_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<GetwellmedcareContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_GetWellMedcare_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<BellContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_BellRehabilitations_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<GorumContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Gorum_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
      services.AddDbContext<BaysideContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Bayside_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
      services.AddDbContext<UptownContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Uptown_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
      services.AddDbContext<TestContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Test_HealthKinect;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
          services.AddDbContext<BrooklynMedicalServicesContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_Brooklyn_Medical_Services;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
              services.AddDbContext<QueensChiropracticAssociatesContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_QueensChiropracticAssociates;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
                  services.AddDbContext<HirschhornLawFirmContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_HirschhornLawFirm;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
                      services.AddDbContext<WellnessDiagnosticImagingPCContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_WellnessDiagnosticImagingPC;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
         //   services.AddDbContext<ApplicationDbContext>(options =>
    //options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=MasterData;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));
    services.AddDbContext<QueensWoundAndMedicalCarePcContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_QueensWoundAndMedicalCarePc;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));   
          services.AddDbContext<OrthoUrgentCareContext>(options =>
    options.UseSqlServer("Data Source=132.148.25.90;Initial catalog=HK_OrthoUrgentCare;MultipleActiveResultSets=True;Persist Security Info=True;User ID=sa;Password=Sheba@003"));   
       
           // services.AddDbContext<DataContext>(x => x.UseInMemoryDatabase("TestDb"));
           // services.AddMvc();
             services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);
            services.AddAutoMapper();

            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var userId = int.Parse(context.Principal.Identity.Name);
                        var user = userService.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configure DI for application services
            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
 app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"StaticFiles")),
                RequestPath = new PathString("/StaticFiles")
            });

            app.UseMvc();
        }
    }
}
