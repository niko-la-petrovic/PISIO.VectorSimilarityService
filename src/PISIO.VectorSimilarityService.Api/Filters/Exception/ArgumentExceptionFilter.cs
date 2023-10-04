using Microsoft.AspNetCore.Mvc.Filters;

namespace PISIO.VectorSimilarityService.Api.Filters.Exception;

public class ArgumentExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public ArgumentExceptionFilter(
        ILogger<ArgumentExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is ArgumentException)
        {
            context.Result = new BadRequestResult();
            context.ExceptionHandled = true;
            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
