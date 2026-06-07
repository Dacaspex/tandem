using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Commands.DeleteTopic;

public sealed record DeleteTopicCommand(Guid TopicId) : ICommand<EmptyResult<DeleteTopicError>>;