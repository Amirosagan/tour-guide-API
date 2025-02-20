namespace Presentation.Contracts.Tours.Common;

public record SessionResponseDto(
    Guid Id,
    DateOnly StartDate,
    DateOnly EndDate,
    int CurrentCapacity
);
