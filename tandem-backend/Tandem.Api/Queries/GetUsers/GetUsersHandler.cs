using Mediator;
using Microsoft.EntityFrameworkCore;
using Tandem.Api.Dtos;
using Tandem.Infrastructure.Database;

namespace Tandem.Api.Queries.GetUsers;

public class GetUsersHandler : IQueryHandler<GetUsersQuery, List<GetUsersDto>>
{
    private readonly TandemContext _context;

    public GetUsersHandler(TandemContext context)
    {
        _context = context;
    }

    public async ValueTask<List<GetUsersDto>> Handle(GetUsersQuery query, CancellationToken ct)
    {
        var users = await _context.Users.ToListAsync(ct);
        return users.Select(Mapper.Map).ToList();
    }
}