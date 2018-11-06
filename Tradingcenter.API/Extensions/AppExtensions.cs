using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tradingcenter.API.Extensions
{
    public static class AppExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
                    builder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;  
                        
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if (error != null)
                        {
                            context.Response.Headers.Add("Application-Error", error.Error.Message);
                            context.Response.Headers.Add("Access-Control-Expose-Headers", "Application-Error");
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            await context.Response.WriteAsync(error.Error.Message);
                        }
                    }));
        }
    }
}
