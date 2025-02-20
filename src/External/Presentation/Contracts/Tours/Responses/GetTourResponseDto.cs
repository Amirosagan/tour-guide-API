using Presentation.Contracts.Tours.Common;

namespace Presentation.Contracts.Tours.Responses;

public record GetTourResponseDto(
    Guid Id,
    string Name,
    string Description,
    double Price,
    string Location,
    string GoogleMapsLocation,
    uint MaxCapacity,
    List<string> Images,
    List<SessionResponseDto> Sessions,
    List<TourCategoryResponseDto> TourCategories
);
