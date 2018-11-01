using ESportStatistics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ESportStatistics.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
            };

            return this.View(errorModel);
        }

        public IActionResult PageNotFound()
        {
            return this.View();
        }

        public IActionResult InternalServerError()
        {
            return this.View();
        }
    }
}
