using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ESportStatistics.Web.Utilities.Middleware
{
    public class PageNotFoundMiddleware
    {
        private readonly RequestDelegate next;

        public PageNotFoundMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);

                if (context.Response.StatusCode == 404)
                {
                    context.Response.Redirect("/404");
                }
            }
            catch (Exception ex)
            {
                context.Response.Redirect("/500");
            }
        }
    }
}
