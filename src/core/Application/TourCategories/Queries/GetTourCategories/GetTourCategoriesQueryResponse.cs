using Domain.Entities;

namespace Application.TourCategories.Queries.GetTourCategories;

public record GetTourCategoriesQueryResponse(
    List<TourCategory> TourCategories
    );