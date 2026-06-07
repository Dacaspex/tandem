using FluentValidation;
using JetBrains.Annotations;

namespace Tandem.Api.Queries.GetTopics;

[UsedImplicitly]
public class GetTopicsValidator : AbstractValidator<GetTopicsQuery>
{
    public GetTopicsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty();
    }
}