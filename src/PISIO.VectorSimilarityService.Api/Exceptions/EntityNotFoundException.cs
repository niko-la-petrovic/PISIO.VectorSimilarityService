using System.Runtime.Serialization;

namespace PISIO.VectorSimilarityService.Api.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }

    public static EntityNotFoundException<TEntity, TIdentifier> Create<TEntity, TIdentifier>(TIdentifier id)
    {
        return new EntityNotFoundException<TEntity, TIdentifier>($"Entity {typeof(TEntity).Name} with id {id} not found");
    }
}

public class EntityNotFoundException<TEntity, TIdentifier> : EntityNotFoundException
{
    public EntityNotFoundException()
    {
    }

    public EntityNotFoundException(string? message) : base(message)
    {
    }

    public EntityNotFoundException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected EntityNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
