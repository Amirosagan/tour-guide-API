using ErrorOr;
using MediatR;

namespace Application.TourCategories.Commands.RemoveTourCategory;

public record RemoveTourCategoryCommand(int Id)
    : IRequest<ErrorOr<RemoveTourCategoryCommandResponse>>;
