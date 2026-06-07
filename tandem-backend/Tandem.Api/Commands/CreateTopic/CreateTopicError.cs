namespace Tandem.Api.Commands.CreateTopic;

public abstract record CreateTopicError
{
    public sealed record TopicGroupNotFound : CreateTopicError;
}