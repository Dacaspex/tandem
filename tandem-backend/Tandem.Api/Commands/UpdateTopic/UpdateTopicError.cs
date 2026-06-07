namespace Tandem.Api.Commands.UpdateTopic;

public abstract record UpdateTopicError
{
    public sealed record UserNotFound : UpdateTopicError;

    public sealed record TopicNotFound : UpdateTopicError;
}