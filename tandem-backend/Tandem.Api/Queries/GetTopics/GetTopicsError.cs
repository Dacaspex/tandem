namespace Tandem.Api.Queries.GetTopics;

public abstract record GetTopicsError
{
    public sealed record UserNotFoundError : GetTopicsError;
}