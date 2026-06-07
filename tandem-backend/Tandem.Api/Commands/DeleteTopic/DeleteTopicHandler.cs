using Mediator;
using Microsoft.EntityFrameworkCore;
using Tandem.Infrastructure.Database;
using Result = Tandem.Api.Common.EmptyResult<Tandem.Api.Commands.DeleteTopic.DeleteTopicError>;

namespace Tandem.Api.Commands.DeleteTopic;

public class DeleteTopicHandler : ICommandHandler<DeleteTopicCommand, Result>
{
    private readonly TandemContext _context;

    public DeleteTopicHandler(TandemContext context)
    {
        _context = context;
    }

    public async ValueTask<Result> Handle(DeleteTopicCommand command, CancellationToken ct)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(t => t.Id == command.TopicId, ct);
        if (topic is null)
        {
            return Result.Fail(new DeleteTopicError.TopicNotFound());
        }

        _context.Topics.Remove(topic);
        await _context.SaveChangesAsync(ct);

        return Result.Success();
    }
}