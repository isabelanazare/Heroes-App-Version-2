
using Fabrit.Heroes.Business.Services;
using Fabrit.Heroes.Business.Services.BackgroundService;
using Fabrit.Heroes.Business.Services.Contracts;
using Fabrit.Heroes.Data;
using Fabrit.Heroes.Data.Business.Authentication.Helpers;
using Fabrit.Heroes.Data.Entities.User;
using Fabrit.Heroes.Data.Mappers;
using Fabrit.Heroes.Infrastructure.Common.Exceptions;
using Fabrit.Heroes.Infrastructure.Common.Password;
using Fabrit.Heroes.Web.Infrastructure;
using Fabrit.Heroes.Web.Infrastructure.Controller.Authentication;
using Fabrit.Heroes.Web.Infrastructure.Middleware;
using Fabrit.Heroes.Web.Infrastructure.Policy;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Fabrit.Heroes.Web.Hubs;

namespace Fabrit.Heroes.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            CurrentEnvironment = environment;
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                // For Development, CORS is required as the backend and frontend have different URLs
                services.AddCors(o => o.AddDefaultPolicy(builder => builder
                    .WithOrigins(Configuration["WebBackendUrl"], Configuration["WebFrontendUrl"])
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                ));
            }

            services.AddControllersWithViews().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            services.AddProblemDetails(config =>
            {
                config.Map<ApiExceptionBase>(exception => new HeroesProblemDetails(exception));
            });
            RegisterAppServices(services);



            services.AddMvc();

            var key = Configuration.GetSection("AppSettings").GetSection("Secret").Value;

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(options =>
            {
                foreach (var role in Enum.GetValues(typeof(RoleType)))
                {
                    options.AddPolicy(role.ToString(), policy => policy.Requirements.Add(new AccountCustomRequirement((RoleType)role)));
                }
            });

            services.AddTransient<IAuthorizationHandler, AccountCustomRequirementHandler>();
            services.AddSignalR();            
        }

        private void RegisterAppServices(IServiceCollection services)
        {

            // Register DB Context and Config4
            services.AddDbContext<HeroesDbContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("HeroesDbConnection")), ServiceLifetime.Scoped);
            services.AddTransient<HeroesDbConfiguration>();

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // Services
            services.AddScoped<IHeroService, HeroService>();
            services.AddScoped<IPowerService, PowerService>();
            services.AddScoped<IElementService, ElementService>();
            services.AddScoped<IChartService, ChartService>();
            services.AddScoped<IHeroTypeService, HeroTypeService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IHashingManager, HashingManager>();
            services.AddScoped<IEmailSenderService, EmailSenderService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IHeroMapper, HeroMapper>();
            services.AddScoped<IVillainService, VillainService>();
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IBattleService, BattleService>();
            services.AddScoped<IHeroPowerService, HeroPowerService>();


            services.AddHostedService<CheckPowerTask>();
            services.AddHostedService<UpdateBadgesTask>();

            services.AddControllers();

            /*
             * Register services here
             */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var heroesDbConfig = serviceScope.ServiceProvider.GetService<HeroesDbConfiguration>();
                heroesDbConfig.Seed();
            }

            /*
             * Register any middleware here
             */
            app.UseExceptionHandler(new ExceptionHandlerOptions
            {
                ExceptionHandler = new ExceptionMiddleware().Invoke
            });

            app.UseCors();

            app.UseHttpsRedirection();



            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
                RequestPath = new PathString("/Images"),
                ServeUnknownFileTypes = true
            });

            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();

            // register authentication
            app.UseAuthorization();
            app.UseAuthentication();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chatsocket");  
            });
        }
    }
}
