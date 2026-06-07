using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Commands.UpdateTopic;

public sealed record UpdateTopicCommand(
    Guid TopicId,
    int Rating
) : ICommand<EmptyResult<UpdateTopicError>>;