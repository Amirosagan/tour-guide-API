using Domain.Entities;

namespace Application.Tours.Queries.GetAllTours;

public record GetAllToursQueryResponse(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNext,
    bool HasPrevious,
    IEnumerable<Tour> Tours
);
