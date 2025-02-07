namespace Presentation.Contracts.Tours.Common;

public record TourInListDto(
    Guid Id,
    string Name,
    string Image,
    double Price,
    string Location
    );