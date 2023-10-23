using BrazilGeographicalData.src.Application.Common.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Text.Json;

namespace BrazilGeographicalData.src.Services.Extensions
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

                    if (exception != null)
                    {
                        context.Response.Headers.Add("Access-Control-Allow-Origin", "*");
                        context.Response.ContentType = "application/json";
                        context.Response.StatusCode = exceptionHandlerFeature.Error switch
                        {
                            BadRequestException => (int)HttpStatusCode.BadRequest,
                            OperationCanceledException => (int)HttpStatusCode.ServiceUnavailable,
                            NotFoundException => (int)HttpStatusCode.NotFound,
                            _ => (int)HttpStatusCode.InternalServerError
                        };

                        var errorResponse = new
                        {
                            StatusCode = context.Response.StatusCode,
                            message = exceptionHandlerFeature.Error.GetBaseException().Message
                        };

                        var jsonErrorResponse = JsonSerializer.Serialize(errorResponse);
                        await context.Response.WriteAsync(jsonErrorResponse);
                    }
                });
            });
        }
    }
}
