using FluentValidation;
using JetBrains.Annotations;

namespace Tandem.Api.Commands.UpdateTopic;

[UsedImplicitly]
public class UpdateTopicValidator : AbstractValidator<UpdateTopicCommand>
{
    public UpdateTopicValidator()
    {
        RuleFor(x => x.TopicId)
            .NotEmpty();

        RuleFor(x => x.Rating)
            .InclusiveBetween(1, 5);
    }
}