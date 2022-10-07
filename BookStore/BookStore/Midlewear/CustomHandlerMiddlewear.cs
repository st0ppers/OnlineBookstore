namespace BookStore.Midlewear
{
    public class CustomHandlerMiddlewear
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomHandlerMiddlewear> _logger;
        public CustomHandlerMiddlewear(RequestDelegate next, ILogger<CustomHandlerMiddlewear> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogError(
                $"Request is from {context.Request.Host.Port.Value} port from {context.Request.Method} ");
            await _next(context);
        }
    }
}
