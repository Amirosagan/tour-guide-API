using Application.TourCategories.Commands.CreateTourCategory;
using Application.TourCategories.Commands.RemoveTourCategory;
using AutoMapper;
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
    }
}
