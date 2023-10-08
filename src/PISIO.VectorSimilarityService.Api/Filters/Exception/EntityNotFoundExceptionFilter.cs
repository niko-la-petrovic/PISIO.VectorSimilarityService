
using Microsoft.AspNetCore.Mvc.Filters;
using PISIO.VectorSimilarityService.Api.Exceptions;

namespace PISIO.VectorSimilarityService.Api.Filters.Exception;

public class EntityNotFoundExceptionFilter : ExceptionFilterAttribute
{
    private readonly ILogger _logger;

    public EntityNotFoundExceptionFilter(
        ILogger<EntityNotFoundExceptionFilter> logger)
    {
        _logger = logger;
    }

    public override void OnException(ExceptionContext context)
    {
        if (context.Exception is EntityNotFoundException)
        {
            context.Result = new NotFoundResult();
            context.ExceptionHandled = true;
            _logger.LogError(context.Exception, context.Exception.Message);
        }
    }
}
