using System.Net;
using Cryptocop.Software.API.Models;
using Cryptocop.Software.API.Models.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using TechnicalRadiation.Models.Exceptions;
using ArgumentOutOfRangeException = System.ArgumentOutOfRangeException;

namespace Cryptocop.Software.API.Extensions
{
    public static class ExceptionMiddlewareExtension
    {
        public static void UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(error =>
            {
                error.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var exception = exceptionHandlerFeature.Error;

                    var statusCode = exception switch
                    {
                        ResourceNotFoundException _ => (int) HttpStatusCode.NotFound,
                        ModelFormatException _ => (int) HttpStatusCode.PreconditionFailed,
                        ArgumentOutOfRangeException _ => (int) HttpStatusCode.BadRequest,
                        ResourceAlreadyExistsException _ => (int) HttpStatusCode.Conflict,
                        InvalidLoginException _ => (int) HttpStatusCode.Unauthorized,
                        _ => (int) HttpStatusCode.InternalServerError
                    };

                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = statusCode;

                    await context.Response.WriteAsync(new ExceptionModel
                    {
                        StatusCode = statusCode,
                        Message = exception.Message
                    }.ToString());

                });
            });
        }
    }
}