using Mediator;

namespace Tandem.Api.Queries.GetUsers;

public sealed record GetUsersQuery : IQuery<List<GetUsersDto>>;