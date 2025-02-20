using Application.TourCategories.Commands.CreateTourCategory;
using Application.TourCategories.Commands.RemoveTourCategory;
using Application.TourCategories.Queries.GetTourCategories;

using AutoMapper;

using Domain.Entities;

using Presentation.Contracts.TourCategories.Common;
using Presentation.Contracts.TourCategories.Requests;
using Presentation.Contracts.TourCategories.Responses;

namespace Presentation.Profiles;

public class TourCategoryProfile : Profile
{
    public TourCategoryProfile()
    {
        CreateMap<CreateTourCategoryDto, CreateTourCategoryCommand>();
        CreateMap<CreateTourCategoryCommandResponse, CreateTourCategoryResponseDto>();

        CreateMap<RemoveTourCategoryDto, RemoveTourCategoryCommand>();
        
        GetAllToursProfile();
    }

    private void GetAllToursProfile()
    {
        CreateMap<GetAllTourCategoriesRequestDto, GetTourCategoriesQuery>();

        CreateMap<GetTourCategoriesQueryResponse, GetAllTourCategoriesResponseDto>();
        
        CreateMap<TourCategory, TourCategoryView>();
    }
}

