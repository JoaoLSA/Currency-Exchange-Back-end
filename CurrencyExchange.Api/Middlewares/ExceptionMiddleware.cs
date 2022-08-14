using CurrencyExchange.Infrastructure;
using FluentValidation.Results;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Net;

namespace CurrencyExchange.Api.Middlewares
{
    internal class ExceptionMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (RequestValidationException ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");
                var result = BuildErrorResponse(ex.Message, HttpStatusCode.BadRequest, ex.Failures);
                await HandleExceptionAsync(httpContext, result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");
                var result = BuildErrorResponse(ex.Message, HttpStatusCode.InternalServerError);
                await HandleExceptionAsync(httpContext, result);
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, ExceptionResponse result)
        {

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = result.Status;
            return context.Response.WriteAsync(JsonConvert.SerializeObject(result,
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }));
        }
        private static ExceptionResponse BuildErrorResponse(string message, HttpStatusCode statusCode)
            => new ()
            {
                Type = "Exception",
                Title = message,
                TraceId = Activity.Current?.Id ?? "Unable to get TraceId",
                Status = (int)statusCode,
                Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "ErrorDetails", new List<string>
                                    {
                                        statusCode.ToString()
                                    }
                    }
                }
            };
        private static ExceptionResponse BuildErrorResponse(string message, HttpStatusCode statusCode, List<ValidationFailure> failures)
             => new()
             {
                 Type = "Exception",
                 Title = message,
                 TraceId = Activity.Current?.Id ?? "Unable to get TraceId",
                 Status = (int)statusCode,
                 Errors = new Dictionary<string, IEnumerable<string>>
                {
                    {
                        "ErrorDetails", failures.Select(x => x.ErrorMessage)
                    }
                }
             };
    }

    internal class ExceptionResponse
    {
        public string? Type { get; set; }
        public string? Title { get; set; }
        public int Status { get; set; }
        public string? TraceId { get; set; }
        public Dictionary<string, IEnumerable<string>>? Errors { get; set; }
    }
}