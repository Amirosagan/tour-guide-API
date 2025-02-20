using Application.Interfaces.UnitOfWork;

using Domain.Entities;

using ErrorOr;

using MediatR;

namespace Application.TourCategories.Queries.GetTourCategories;

public class GetTourCategoriesHandler : IRequestHandler<GetTourCategoriesQuery, ErrorOr<GetTourCategoriesQueryResponse>>
{
    private readonly ITourCategoryRepository _tourCategoryRepository;

    public GetTourCategoriesHandler(ITourCategoryRepository tourCategoryRepository)
    {
        _tourCategoryRepository = tourCategoryRepository;
    }
    
    public async Task<ErrorOr<GetTourCategoriesQueryResponse>> Handle(GetTourCategoriesQuery request, CancellationToken cancellationToken)
    {
        var tourCategories = await _tourCategoryRepository.GetAllAsync();
        
        return new GetTourCategoriesQueryResponse(tourCategories);
    }
}