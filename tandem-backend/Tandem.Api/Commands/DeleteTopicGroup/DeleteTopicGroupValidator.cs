using FluentValidation;
using JetBrains.Annotations;

namespace Tandem.Api.Commands.DeleteTopicGroup;

[UsedImplicitly]
public class DeleteTopicGroupValidator : AbstractValidator<DeleteTopicGroupCommand>
{
    public DeleteTopicGroupValidator()
    {
        RuleFor(x => x.TopicGroupId)
            .NotEmpty();
    }
}