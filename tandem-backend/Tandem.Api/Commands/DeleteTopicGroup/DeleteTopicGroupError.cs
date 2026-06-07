namespace Tandem.Api.Commands.DeleteTopicGroup;

public abstract record DeleteTopicGroupError
{
    public record TopicGroupNotFound : DeleteTopicGroupError;
}