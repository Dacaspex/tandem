using JetBrains.Annotations;

namespace Tandem.Api.Commands.CreateTopicGroup;

[UsedImplicitly]
public sealed record CreateTopicGroupDto(Guid Id, string Name);