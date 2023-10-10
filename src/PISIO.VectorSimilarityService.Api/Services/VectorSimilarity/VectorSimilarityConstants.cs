namespace PISIO.VectorSimilarityService.Api.Services.VectorSimilarity;

public static class VectorSimilarityConstants
{
    public static class Algorithm
    {
        public const string Flat = "flat";
        public const string LSH = "lsh";
        public const string IVF = "ivf";
        public const string HNSW = "hnsw";
        public static readonly IReadOnlyList<string> Algorithms = new List<string> { Flat, LSH, IVF, HNSW };
    }

    public static class Distance
    {
        public const string L2 = "l2";
        public const string IP = "ip";
        public const string Cosine = "cosine";
        public static readonly IReadOnlyList<string> Distances = new List<string> { L2, IP, Cosine };
    }
}
