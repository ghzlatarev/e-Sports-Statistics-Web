using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ESportStatistics.Web.Utilities.Middleware;
using Microsoft.AspNetCore.Builder;

namespace ESportStatistics.Web.Utilities.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseNotFoundExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<PageNotFoundMiddleware>();
        }

        public static void UseInternalServerErrorExceptionHandler(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<InternalServerErrorMiddleware>();
        }
    }
}
