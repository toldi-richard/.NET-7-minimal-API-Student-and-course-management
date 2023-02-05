namespace StudentEnrollment.API.Filters;

public class LoggingFilter : IEndpointFilter
{
    private readonly ILogger _logger;
    public LoggingFilter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<LoggingFilter>();
    }
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var method = context.HttpContext.Request.Method;
        var path = context.HttpContext.Request.Path;

        _logger.LogInformation($"{method} request made to {path}");

        try
        {
            var result = await next(context);
            _logger.LogInformation($"{method} request made to {path} succesful");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"{method} Request to {path} failed.");
            return TypedResults.Problem("An Error has Occured, please try again later.");
        }
    }
}
