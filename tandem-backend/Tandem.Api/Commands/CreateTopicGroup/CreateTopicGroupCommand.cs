using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Commands.CreateTopicGroup;

public sealed record CreateTopicGroupCommand(string Name) : ICommand<Result<CreateTopicGroupDto>>;