using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ESportStatistics.Web.Utilities.Middleware
{
    public class InternalServerErrorMiddleware
    {
        private readonly RequestDelegate _next;

        public InternalServerErrorMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                if (context.Response.StatusCode == 500)
                {
                    context.Response.Redirect("/500");
                }
            }
            catch (Exception ex)
            {
                context.Response.Redirect("/500");
            }
        }
    }
}
