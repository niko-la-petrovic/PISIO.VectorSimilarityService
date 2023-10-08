using Microsoft.AspNetCore.Mvc.Filters;

namespace PISIO.VectorSimilarityService.Api.Filters.Exception;

public class InvalidOperationExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public InvalidOperationExceptionFilter(ILogger<InvalidOperationExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is InvalidOperationException ex)
        {
            context.ExceptionHandled = true;
            context.Result = new BadRequestResult();
            _logger.LogError(ex, "Invalid operation");
        }
    }
}
