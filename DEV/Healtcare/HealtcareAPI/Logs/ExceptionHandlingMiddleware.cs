using System.Net;
using System.Text.Json;

namespace HealthcareAPI.Logs
{
    /// <summary>
    /// Captures errors and saves them to a local file
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly string _logFilePath;

        /// <summary>
        /// write the errors to the api-error.log file in a physical path. 
        /// </summary>
        /// <see cref="IConfiguration">
        /// "LogSettings:LogFilePath"       : local routine where the log configured in appsetting.json is stored
        /// </see>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="config"></param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _logFilePath = config["LogSettings:LogFilePath"] ?? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "api-errors.log");
        }

        /// <summary>
        /// captures the invocation
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        /// <summary>
        /// captures the error and writes it to the file
        /// </summary>
        /// <param name="context"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var statusCode = ex switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var errorDetails = new
            {
                Message = "An error occurred while processing your request.",
                Error = ex.Message,
                StatusCode = statusCode,
                Timestamp = DateTime.UtcNow
            };

            var errorJson = JsonSerializer.Serialize(errorDetails);

            // Log en archivo con formato mejorado
            var logEntry = $"{DateTime.UtcNow}: {ex.GetType()} - {ex.Message}\n{ex.StackTrace}\n";
            await File.AppendAllTextAsync(_logFilePath, logEntry);
           
            // Log en consola
            _logger.LogError(ex, "Unhandled exception occurred");

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(errorJson);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public static class ExceptionHandlingMiddlewareExtensions
    {
        /// <summary>
        /// Exception handler extension
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }

        /// <summary>
        /// Extension of domains authorised to access the service. Productive environment only
        /// </summary>
        /// <see cref="IConfiguration">
        /// "Cors:AllowedOrigins"       : domains authorised to access the service, configurable in appsetting.json
        /// </see>
        /// <param name="services"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureCorsFromConfig(this IServiceCollection services, IConfiguration config)
        {
            var allowedOrigins = config.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? new string[0];
            //write the domains configured for the cors in the console
            Console.WriteLine("Allowed Origins: " + string.Join(", ", allowedOrigins));

            services.AddCors(options =>
            {
                options.AddPolicy("ConfiguredCorsPolicy", builder =>
                    builder.WithOrigins(allowedOrigins)
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());

                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });

            });

            return services;
        }
    }
}
