using Application.Interfaces.UnitOfWork;

using MediatR;
using ErrorOr;

namespace Application.Tours.Queries.GetAllTours;

public class GetAllToursQueryHandler : IRequestHandler<GetAllToursQuery, ErrorOr<GetAllToursQueryResponse>>
{
    private readonly ITourRepository _tourRepository;
    public GetAllToursQueryHandler(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }

    public async Task<ErrorOr<GetAllToursQueryResponse>> Handle(GetAllToursQuery request, CancellationToken cancellationToken)
    {
        var tours = _tourRepository.GetQueryable();

        if (request.SearchTerm != null)
        {
            tours = tours.Where(t => t.Name.Contains(request.SearchTerm) || t.Description.Contains(request.SearchTerm) || t.Location.Contains(request.SearchTerm));
        }

        if (request.CategoryIds is { Count: > 0 })
        {
            tours = tours.Where(t => t.Categories.Any(c => request.CategoryIds.Contains(c.Id)));
        }


        var pagedTours = await PagedTour.Create(tours, request.PageNumber, request.PageSize);

        return new GetAllToursQueryResponse(
            Tours: pagedTours.Items,
            PageNumber: request.PageNumber,
            PageSize: request.PageSize,
            TotalCount: pagedTours.Count,
            TotalPages: pagedTours.TotalPages,
            HasNext: pagedTours.HasNextPage,
            HasPrevious: pagedTours.HasPreviousPage
        );
    }
}