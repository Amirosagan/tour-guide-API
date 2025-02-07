namespace Presentation.Contracts.Tours.Requests;

public record GetAllToursRequestDto(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    List<int>? CategoryIds = null
);