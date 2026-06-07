namespace Tandem.Api.Commands.DeleteTopic;

public abstract record DeleteTopicError
{
    public record TopicNotFound : DeleteTopicError;
}