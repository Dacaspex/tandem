namespace Tandem.Api.Commands.CreateTopic;

public sealed record CreateTopicDto(
    Guid Id,
    string Name,
    int Rating);