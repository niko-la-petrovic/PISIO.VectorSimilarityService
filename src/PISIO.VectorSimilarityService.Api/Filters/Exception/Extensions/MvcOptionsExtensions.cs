using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Immutable;

namespace PISIO.VectorSimilarityService.Api.Filters.Exception.Extensions;

public static class MvcOptionsExtensions
{
    public static MvcOptions AddExceptionFilters(this MvcOptions options)
    {
        var filters = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => a.GetTypes())
        .Where(t =>
            !t.IsAssignableFrom(typeof(ExceptionFilterAttribute)) &&
            t.IsAssignableTo(typeof(ExceptionFilterAttribute)))
        .ToImmutableList();
        filters.ForEach(f => options.Filters.Add(f));
        return options;
    }
}
