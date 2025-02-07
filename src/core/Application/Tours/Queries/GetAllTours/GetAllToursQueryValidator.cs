using FluentValidation;

namespace Application.Tours.Queries.GetAllTours;

public class GetAllToursQueryValidator : AbstractValidator<GetAllToursQuery>
{
    public GetAllToursQueryValidator()
    {
        RuleFor(x => x.PageNumber).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
        RuleForEach(x => x.CategoryIds).GreaterThan(0);
    }
}
