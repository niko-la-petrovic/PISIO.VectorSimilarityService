﻿namespace PISIO.VectorSimilarityService.Api.Dtos;

public class Paginated<T>
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public int? TotalCount { get; set; }
    public int? TotalPages { get; set; }
    public IEnumerable<T> Items { get; set; } = new List<T>();
}