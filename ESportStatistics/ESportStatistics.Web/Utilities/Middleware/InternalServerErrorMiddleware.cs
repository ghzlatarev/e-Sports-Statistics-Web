using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ESportStatistics.Web.Utilities.Middleware
{
    public class InternalServerErrorMiddleware
    {
        private readonly RequestDelegate next;

        public InternalServerErrorMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await this.next.Invoke(context);

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
