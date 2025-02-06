using Presentation.Contracts.Tours.Common;

namespace Presentation.Contracts.Tours.Requests;

public record CreateTourDto(
    string Name,
    string Description,
    double Price,
    string Location,
    List<string> Images,
    List<SessionDto> Sessions,
    List<int> TourCategoryIds,
    uint MaxCapacity
);
