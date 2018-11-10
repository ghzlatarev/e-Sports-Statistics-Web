using ESportStatistics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ESportStatistics.Web.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("about")]
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult About()
        {
            return View();
        }

        [HttpGet("contact")]
        [ResponseCache(CacheProfileName = "Default")]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpGet("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
