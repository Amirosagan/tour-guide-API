using Application.Interfaces.UnitOfWork;
using Domain.Entities;
using Domain.Errors;
using ErrorOr;
using MediatR;

namespace Application.Tours.Queries.GetTour;

public class GetTourQueryHandler : IRequestHandler<GetTourQuery, ErrorOr<Tour>>
{
    private readonly ITourRepository _tourRepository;

    public GetTourQueryHandler(ITourRepository tourRepository)
    {
        _tourRepository = tourRepository;
    }

    public async Task<ErrorOr<Tour>> Handle(
        GetTourQuery request,
        CancellationToken cancellationToken
    )
    {
        var tour = await _tourRepository.GetAsyncIncludeSessionsAndCategories(request.Id);

        if (tour is null)
        {
            return DomainErrors.Tour.TourNotFound();
        }

        return tour;
    }
}
