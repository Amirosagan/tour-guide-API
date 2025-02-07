using FluentValidation;

namespace Application.Tours.Commands.CreateTour;

public class CreateTourCommandValidator : AbstractValidator<CreateTourCommand>
{
    public CreateTourCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.Description).NotEmpty();
        RuleFor(x => x.Price).NotEmpty();
        RuleForEach(x => x.Sessions).Must((command, session) => session.StartDate <= session.EndDate).WithMessage("Start date must be before or equal end date");
        RuleForEach(x=>x.Sessions).Must((sessions => sessions.StartDate >= DateOnly.FromDateTime(DateTime.Now) && sessions.EndDate >=DateOnly.FromDateTime(DateTime.Now))).WithMessage("Start date and end date must be in the future");
    }
}