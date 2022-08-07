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
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred");
                await HandleExceptionAsync(httpContext, "Internal Server Error", HttpStatusCode.InternalServerError);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, string message, HttpStatusCode statusCode)
        {
            var result = BuildErrorResponse(message, statusCode);

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
                Errors = new Dictionary<string, List<string>>
                {
                    {
                        "ErrorDetails", new List<string>
                                    {
                                        statusCode.ToString()
                                    }
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
        public Dictionary<string, List<string>>? Errors { get; set; }
    }
}