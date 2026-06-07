using FluentValidation;

namespace Tandem.Api.Commands.CreateTopicGroup;

public class CreateTopicGroupValidator : AbstractValidator<CreateTopicGroupCommand>
{
    public CreateTopicGroupValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty();
    }
}