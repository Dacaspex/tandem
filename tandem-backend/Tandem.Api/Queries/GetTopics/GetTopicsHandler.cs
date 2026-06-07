using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tandem.Api.Common;
using Tandem.Api.Dtos;
using Tandem.Domain.Users;
using Tandem.Infrastructure.Database;

namespace Tandem.Api.Queries.GetTopics;

public class GetTopicsHandler : IQueryHandler<GetTopicsQuery, ListResult<TopicGroupDto, GetTopicsError>>
{
    private readonly TandemContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public GetTopicsHandler(TandemContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async ValueTask<ListResult<TopicGroupDto, GetTopicsError>> Handle(
        GetTopicsQuery query,
        CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(query.UserId);
        if (user == null)
        {
            return ListResult<TopicGroupDto, GetTopicsError>.Fail(new GetTopicsError.UserNotFoundError());
        }

        var topicGroups = await _context.TopicGroups
            .Include(tg => tg.Topics.OrderBy(t => t.Name))
            .Where(tg => tg.UserId == query.UserId)
            .ToListAsync(ct);

        var dtos = topicGroups.Select(Mapper.Map).ToList();

        return ListResult<TopicGroupDto, GetTopicsError>.Success(dtos);
    }
}