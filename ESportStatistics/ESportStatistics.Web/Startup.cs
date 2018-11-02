using ESportStatistics.Core.Providers;
using ESportStatistics.Core.Providers.Contracts;
using ESportStatistics.Core.Services;
using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Data.Context;
using ESportStatistics.Data.Models.Identity;
using ESportStatistics.Services.Data.Services.Identity;
using ESportStatistics.Services.Data.Services.Identity.Contracts;
using ESportStatistics.Services.External;
using ESportStatistics.Web.Utilities.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace ESportStatistics.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("DevelopmentConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<HttpClient>();
            services.AddScoped<IPandaScoreClient, PandaScoreClient>();

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

            services.AddScoped<IEmailSender, EmailSender>();

            services.AddMvc();
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

                routes.MapAreaRoute(
                    name: "register",
                    areaName: "Identity",
                    template: "register",
                    defaults: new { controller = "Account", action = "Register" });

                routes.MapAreaRoute(
                    name: "login",
                    areaName: "Identity",
                    template: "login",
                    defaults: new { controller = "Account", action = "Login" });

                routes.MapAreaRoute(
                    name: "logout",
                    areaName: "Identity",
                    template: "logout",
                    defaults: new { controller = "Account", action = "Logout" });

                routes.MapAreaRoute(
                    name: "profile",
                    areaName: "Identity",
                    template: "profile",
                    defaults: new { controller = "Manage", action = "Index" });

                routes.MapAreaRoute(
                    name: "changepassword",
                    areaName: "Identity",
                    template: "changepassword",
                    defaults: new { controller = "Manage", action = "ChangePassword" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
