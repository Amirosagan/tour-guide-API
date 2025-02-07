using ErrorOr;
using MediatR;

namespace Application.Tours.Queries.GetAllTours;

public record GetAllToursQuery(
    int PageNumber = 1,
    int PageSize = 10,
    string? SearchTerm = null,
    List<int>? CategoryIds = null
) : IRequest<ErrorOr<GetAllToursQueryResponse>>;
