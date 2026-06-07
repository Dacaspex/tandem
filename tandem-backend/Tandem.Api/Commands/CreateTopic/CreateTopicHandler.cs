using Mediator;
using Microsoft.EntityFrameworkCore;
using Tandem.Domain.Topics;
using Tandem.Infrastructure.Database;
using Result = Tandem.Api.Common.Result<
    Tandem.Api.Commands.CreateTopic.CreateTopicDto,
    Tandem.Api.Commands.CreateTopic.CreateTopicError>;

namespace Tandem.Api.Commands.CreateTopic;

public class CreateTopicHandler : ICommandHandler<CreateTopicCommand, Result>
{
    private readonly TandemContext _context;

    public CreateTopicHandler(TandemContext context)
    {
        _context = context;
    }

    public async ValueTask<Result> Handle(
        CreateTopicCommand command,
        CancellationToken ct)
    {
        var topicGroupExists = await _context.TopicGroups.AnyAsync(g => g.Id == command.TopicGroupId, ct);
        if (!topicGroupExists)
        {
            return Result.Fail(new CreateTopicError.TopicGroupNotFound());
        }

        var topic = Topic.Create(command.Name, command.TopicGroupId);

        await _context.Topics.AddAsync(topic, ct);
        await _context.SaveChangesAsync(ct);

        var dto = new CreateTopicDto(topic.Id, topic.Name, topic.Rating);

        return Result.Success(dto);
    }
}