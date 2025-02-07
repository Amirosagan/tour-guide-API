using ErrorOr;
using MediatR;

namespace Application.TourCategories.Commands.CreateTourCategory;

public record CreateTourCategoryCommand(string Name)
    : IRequest<ErrorOr<CreateTourCategoryCommandResponse>>;
