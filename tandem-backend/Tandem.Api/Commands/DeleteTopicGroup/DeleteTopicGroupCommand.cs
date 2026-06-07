using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Commands.DeleteTopicGroup;

public sealed record DeleteTopicGroupCommand(Guid TopicGroupId) : ICommand<EmptyResult<DeleteTopicGroupError>>;