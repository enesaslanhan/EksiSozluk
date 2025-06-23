using BlazorSozluk.Common.Infrastructure.Exceptions;
using BlazorSozluk.Common.Infrastructure.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BlazorSozluk.Api.WebApi.Infrastructure.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureExceptionHandling(this IApplicationBuilder app,
                                           bool inclueExceptionDetails = false,
                                           bool useDefaultHandlingResponse = true,
                                           Func<HttpContext, Exception, Task> handleException = null)
        {
            app.UseExceptionHandler(options =>
            {


                options.Run(context =>
                {
                    var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
                    if (!useDefaultHandlingResponse && handleException == null)
                        throw new ArgumentNullException(nameof(handleException),
                            $"{nameof(handleException)} connat be null when {nameof(useDefaultHandlingResponse)} is false");

                    if (!useDefaultHandlingResponse && handleException != null)
                        return handleException(context, exceptionObject.Error);
                    return DefaultHandleException(context, exceptionObject.Error, inclueExceptionDetails);
    
                });
            });
            return app;
        }

        private static async Task DefaultHandleException(HttpContext context, Exception exception, bool inclueExceptionDetails)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal server error occured!";

            if (exception is UnauthorizedAccessException)
                statusCode = HttpStatusCode.Unauthorized;
            if (exception is DatabaseValidateException)
            {
                var validationResponse = new ValidationResponseModel(exception.Message);
                await WriteResponse(context, statusCode, validationResponse);
                return;
            }
            var res = new
            {
                HttpStatusCode = (int)statusCode,
                Details = inclueExceptionDetails ? exception.ToString() : message,

            };
            await WriteResponse(context, statusCode, res);
        }
        private static async Task WriteResponse(HttpContext context, HttpStatusCode statusCode, object responseObj)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            await context.Response.WriteAsJsonAsync(responseObj);
        }
    }
}
