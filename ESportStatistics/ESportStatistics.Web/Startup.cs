using ESportStatistics.Core.Providers;
using ESportStatistics.Core.Providers.Contracts;
using ESportStatistics.Core.Services;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services;
using ESportStatistics.Services.Contracts;
using ESportStatistics.Services.Data.Services.Identity;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Services.External;
using ESportStatistics.Web.Utilities.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace ESportStatistics.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            this.Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            RegisterData(services);

            RegisterAuthentication(services);

            RegisterServices(services);
            RegisterServicesExternal(services);
            RegisterServicesData(services);

            RegisterInfrastructure(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseNotFoundExceptionHandler();

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "internalservererror",
                    template: "500",
                    defaults: new { controller = "Error", action = "InternalServerError" });

                routes.MapRoute(
                    name: "notfound",
                    template: "404",
                    defaults: new { controller = "Error", action = "PageNotFound" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}");
            });
        }

        private void RegisterData(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddDbContext<DataContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));
            }
            else
            {
                services.AddDbContext<DataContext>(options =>
                     options.UseSqlServer(System.Environment.GetEnvironmentVariable("AZURE_ESS_DB_Connection" ,EnvironmentVariableTarget.User)));
            }
        }

        private void RegisterAuthentication(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            if (this.Environment.IsDevelopment())
            {
                services.Configure<IdentityOptions>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequiredUniqueChars = 0;

                    // Lockout settings
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(1);
                    options.Lockout.MaxFailedAccessAttempts = 999;
                });
            }

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Admin", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("Administrator");
                });

                options.AddPolicy("Default", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireRole("User", "Administrator");
                });
            });
        }

        private void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IPDFService, PDFService>();
        }

        private void RegisterServicesExternal(IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<IPandaScoreClient, PandaScoreClient>();
        }

        private void RegisterServicesData(IServiceCollection services)
        {
            // Identity
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IChampionService, ChampionService>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IMasteryService, MasteryService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<ISerieService, SerieService>();
            services.AddScoped<ISpellService, SpellService>();
            services.AddScoped<ITournamentService, TournamentService>();
        }

        private void RegisterInfrastructure(IServiceCollection services)
        {
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddResponseCaching();
            services.AddMemoryCache();

            services.AddMvc(options =>
            {
                options.CacheProfiles.Add("Default",
                    new CacheProfile()
                    {
                        Duration = 60
                    });

                options.CacheProfiles.Add("Short",
                    new CacheProfile()
                    {
                        Duration = 30
                    });
            });
        }
    }
}
