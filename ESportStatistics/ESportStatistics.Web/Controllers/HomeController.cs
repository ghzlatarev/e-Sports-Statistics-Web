using ESportStatistics.Core.Services.Contracts;
using ESportStatistics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IChampionService service;
        private readonly IConfiguration configuration;

        public HomeController(IConfiguration configuration, IChampionService service)
        {
            this.service = service;
            this.configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            await this.service.RebaseChampionsAsync(configuration["PandaScoreAPIAccessToken"]);
            await this.service.DeleteChampionAsync("Sion");
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
