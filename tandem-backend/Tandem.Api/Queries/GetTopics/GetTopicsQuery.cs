using Mediator;
using Tandem.Api.Common;

namespace Tandem.Api.Queries.GetTopics;

public sealed record GetTopicsQuery(string UserId)
    : IQuery<ListResult<TopicGroupDto, GetTopicsError>>;