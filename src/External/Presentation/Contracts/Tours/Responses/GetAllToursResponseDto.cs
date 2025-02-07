using Presentation.Contracts.Tours.Common;

namespace Presentation.Contracts.Tours.Responses;

public record GetAllToursResponseDto(
    int PageNumber,
    int PageSize,
    int TotalCount,
    int TotalPages,
    bool HasNext,
    bool HasPrevious,
    IEnumerable<TourInListDto> Tours
);
