using FluentValidation;

namespace Tandem.Api.Commands.CreateTopic;

public class CreateTopicValidator : AbstractValidator<CreateTopicCommand>
{
    public CreateTopicValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();

        RuleFor(x => x.TopicGroupId)
            .NotEmpty();
    }
}