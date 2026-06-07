using Mediator;
using Tandem.Domain.Topics;
using Tandem.Domain.Users;
using Tandem.Infrastructure.Database;
using Result = Tandem.Api.Common.Result<Tandem.Api.Commands.CreateTopicGroup.CreateTopicGroupDto>;

namespace Tandem.Api.Commands.CreateTopicGroup;

public class CreateTopicGroupHandler : ICommandHandler<CreateTopicGroupCommand, Result>
{
    private readonly TandemContext _context;
    private readonly ICurrentUser _currentUser;

    public CreateTopicGroupHandler(TandemContext context, ICurrentUser currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async ValueTask<Result> Handle(CreateTopicGroupCommand command, CancellationToken ct)
    {
        var topicGroup = TopicGroup.Create(command.Name, _currentUser.UserId);

        await _context.TopicGroups.AddAsync(topicGroup, ct);
        await _context.SaveChangesAsync(ct);

        var dto = new CreateTopicGroupDto(topicGroup.Id, topicGroup.Name);

        return Result.Success(dto);
    }
}