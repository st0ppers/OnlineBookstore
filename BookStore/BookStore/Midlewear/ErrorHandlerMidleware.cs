using System.Net;

namespace BookStore.Midlewear
{
    public class ErrorHandlerMidleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMidleware> _logger;
        public ErrorHandlerMidleware(RequestDelegate next, ILogger<ErrorHandlerMidleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        _logger.LogError($"  {error.Message}");
                        break;
                    case CustomException e:
                        //custom application error
                        response.StatusCode = (int)HttpStatusCode.BadGateway;
                        _logger.LogError($"  {error.Message}");
                        break;
                    case KeyNotFoundException e:
                        //not found error
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        // unhandled error
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;

                }

                //var result = JsonConvert.SerializeObject(new {message = error.Message});
                await response.WriteAsJsonAsync(new { message = error.Message });


            }
        }
    }
}
