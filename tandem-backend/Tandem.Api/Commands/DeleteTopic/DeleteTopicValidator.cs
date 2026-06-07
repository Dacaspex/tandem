using FluentValidation;

namespace Tandem.Api.Commands.DeleteTopic;

public class DeleteTopicValidator : AbstractValidator<DeleteTopicCommand>
{
    public DeleteTopicValidator()
    {
        RuleFor(x => x.TopicId)
            .NotEmpty();
    }
}