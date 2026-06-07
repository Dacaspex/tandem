using Mediator;
using Microsoft.EntityFrameworkCore;
using Tandem.Infrastructure.Database;
using Result = Tandem.Api.Common.EmptyResult<Tandem.Api.Commands.DeleteTopicGroup.DeleteTopicGroupError>;

namespace Tandem.Api.Commands.DeleteTopicGroup;

public class DeleteTopicGroupHandler : ICommandHandler<DeleteTopicGroupCommand, Result>
{
    private readonly TandemContext _context;

    public DeleteTopicGroupHandler(TandemContext context)
    {
        _context = context;
    }

    public async ValueTask<Result> Handle(DeleteTopicGroupCommand command, CancellationToken ct)
    {
        var topicGroup = await _context.TopicGroups.FirstOrDefaultAsync(t => t.Id == command.TopicGroupId, ct);
        if (topicGroup is null)
        {
            return Result.Fail(new DeleteTopicGroupError.TopicGroupNotFound());
        }

        _context.TopicGroups.Remove(topicGroup);
        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}