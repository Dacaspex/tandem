using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Commands.CreateTopic;

public sealed record CreateTopicCommand(
    Guid TopicGroupId,
    string Name
) : ICommand<Result<CreateTopicDto, CreateTopicError>>;