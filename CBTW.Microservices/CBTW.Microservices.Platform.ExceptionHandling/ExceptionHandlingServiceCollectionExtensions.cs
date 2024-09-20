using CBTW.Microservices.Platform.ExceptionHandling;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net;

namespace Microsoft.Extensions.DependencyInjection;

public static class ExceptionHandlingServiceCollectionExtensions
{
    public static IApplicationBuilder UsePlatformExceptionHandling(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(options =>
        {
            options.Run(async context =>
            {
                var ex = context.Features.Get<IExceptionHandlerFeature>();
                if (ex?.Error != null)
                {
                    if (ex.Error is PlatformResponseException)
                    {
                        var hrex = ex.Error as PlatformResponseException;
                        context.Response.StatusCode = (int)hrex.Response.StatusCode;
                        context.Response.ContentType = "application/json";
                        var err = JsonConvert.SerializeObject(new { error = ex.Error.Message });
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var err = JsonConvert.SerializeObject(new { error = ex.Error.Message });
                        await context.Response.WriteAsync(err).ConfigureAwait(false);
                    }
                }
            });
        });

        return app;
    }
}
