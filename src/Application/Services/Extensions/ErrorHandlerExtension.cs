using BrazilGeographicalData.src.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace BrazilGeographicalData.src.Application.Services.Extensions
{
    public static class ErrorHandlerExtension
    {
        public static void UseErrorHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature?.Error;
                    if (exceptionHandlerFeature == null) return;

                    context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                    context.Response.ContentType = "application/json";

                    if (exception is BadRequestException badRequestException)
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                        var errorResponse = new
                        {
                            Message = badRequestException.Message,
                            StatusCode = context.Response.StatusCode
                        };

                        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                        await context.Response.WriteAsync(jsonErrorResponse);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        var errorResponse = new
                        {
                            Message = exception.Message,
                            StatusCode = context.Response.StatusCode
                        };

                        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                        await context.Response.WriteAsync(jsonErrorResponse);
                    }
                });
            });
        }
    }


}
