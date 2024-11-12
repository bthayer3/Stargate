using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;

namespace StargateAPI.Business.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<RequestLoggingMiddleware> _logger;

        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/swagger"))
            {
                // //Swagger will accidentally trigger a log entry, skip logging for swagger and continue the request pipeline
                await _next(context);
                return;
            }

            var endpoint = context.GetEndpoint();
            var endpointName = endpoint?.DisplayName.Replace(" (StargateAPI)", "") ?? "Unknown Endpoint";
            var logEntry = new RequestLog
            {
                Endpoint = endpointName,
                HttpMethod = context.Request.Method,
                RequestTime = DateTime.UtcNow
            };

            try
            {
                await _next(context);

                logEntry.StatusCode = context.Response.StatusCode;
                logEntry.IsSuccess = (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300);
            }
            catch (Exception ex)
            {
                logEntry.IsSuccess = false;
                logEntry.StatusCode = 500;
                logEntry.ExceptionMessage = ex.Message;
            }
            finally
            {
                logEntry.ResponseTime = DateTime.UtcNow;

                // Create a scope to resolve the scoped service StargateContext
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<StargateContext>();

                    dbContext.RequestLog.Add(logEntry);
                    await dbContext.SaveChangesAsync();
                }
            }
        }
    }
}