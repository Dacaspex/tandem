using Mediator;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tandem.Domain.Users;
using Tandem.Infrastructure.Database;
using Result = Tandem.Api.Common.EmptyResult<Tandem.Api.Commands.UpdateTopic.UpdateTopicError>;

namespace Tandem.Api.Commands.UpdateTopic;

public class UpdateTopicHandler : ICommandHandler<UpdateTopicCommand, Result>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ICurrentUser _currentUser;
    private readonly TandemContext _context;

    public UpdateTopicHandler(UserManager<ApplicationUser> userManager, ICurrentUser currentUser, TandemContext context)
    {
        _userManager = userManager;
        _currentUser = currentUser;
        _context = context;
    }

    public async ValueTask<Result> Handle(UpdateTopicCommand command, CancellationToken ct)
    {
        var user = await _userManager.FindByIdAsync(_currentUser.UserId);
        if (user is null)
        {
            return Result.Fail(new UpdateTopicError.UserNotFound());
        }

        var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == command.TopicId, ct);
        if (topic is null)
        {
            return Result.Fail(new UpdateTopicError.TopicNotFound());
        }

        topic.Rating = command.Rating;

        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}