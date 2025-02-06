using Application.Tours.Helpers;
using ErrorOr;
using MediatR;

namespace Application.Tours.Commands.CreateTour;

public record CreateTourCommand(
    string Name,
    string Description,
    double Price,
    string Location,
    List<string> Images,
    List<SessionInRecords> Sessions,
    List<int> TourCategoryIds,
    uint MaxCapacity
) : IRequest<ErrorOr<CreateTourCommandResponse>>;
