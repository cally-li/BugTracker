using BugTracker.Errors;
using System.Net;
using System.Text.Json;

//middleware to catch exceptions globally
namespace BugTracker.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        
        //ILogger used to enable error logging in terminal
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        //middleware occurs in the context of an http request 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                //process http request/pass on to next piece of middleware
                await _next(context); 
            }
            catch (Exception ex)
            {
                //log error in terminal
                 _logger.LogError(ex, ex.Message);

                //set content-type and status code in the response header for the http request
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                //generate response based on production or development env
                var response = _env.IsDevelopment() ?
                    //in development env -shows stack trace
                    new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    //in production env
                    : new ApiException(context.Response.StatusCode, "Internal Server Error");

                //write the response in JSON and camel case
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);



            }
        }
    }
}
