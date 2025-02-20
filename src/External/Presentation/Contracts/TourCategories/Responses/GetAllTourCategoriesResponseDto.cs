using Presentation.Contracts.TourCategories.Common;

namespace Presentation.Contracts.TourCategories.Responses;

public record GetAllTourCategoriesResponseDto(
    List <TourCategoryView> TourCategories
);