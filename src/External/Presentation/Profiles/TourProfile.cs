using Application.Tours.Commands.CreateTour;
using Application.Tours.Helpers;
using Application.Tours.Queries.GetAllTours;
using Application.Tours.Queries.GetTour;
using AutoMapper;
using Domain.Entities;
using Domain.Errors;
using Presentation.Contracts.TourCategories.Responses;
using Presentation.Contracts.Tours.Common;
using Presentation.Contracts.Tours.Requests;
using Presentation.Contracts.Tours.Responses;

namespace Presentation.Profiles;

public class TourProfile : Profile
{
    public TourProfile()
    {
        CreateMap<CreateTourDto, CreateTourCommand>();

        CreateMap<SessionDto, SessionInRecords>();

        CreateMap<CreateTourCommandResponse, CreateTourResponseDto>();

        GetAllToursProfile();

        GetTourProfile();
    }

    private void GetAllToursProfile()
    {
        CreateMap<GetAllToursRequestDto, GetAllToursQuery>();

        CreateMap<Tour, TourInListDto>()
            .ForCtorParam(nameof(TourInListDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(TourInListDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(TourInListDto.Price), opt => opt.MapFrom(src => src.Price))
            .ForCtorParam(nameof(TourInListDto.Location), opt => opt.MapFrom(src => src.Location))
            .ForCtorParam(
                nameof(TourInListDto.Image),
                opt => opt.MapFrom(src => src.Images.FirstOrDefault())
            );

        CreateMap<GetAllToursQueryResponse, GetAllToursResponseDto>();
    }

    private void GetTourProfile()
    {
        CreateMap<GetTourRequestDto, GetTourQuery>();

        CreateMap<Session, SessionResponseDto>()
            .ForCtorParam(
                nameof(SessionResponseDto.StartDate),
                opt => opt.MapFrom(src => src.StartDate)
            )
            .ForCtorParam(
                nameof(SessionResponseDto.EndDate),
                opt => opt.MapFrom(src => src.EndDate)
            )
            .ForCtorParam(
                nameof(SessionResponseDto.CurrentCapacity),
                opt => opt.MapFrom(src => src.CurrentCapacity)
            );

        CreateMap<TourCategory, CreateTourCategoryResponseDto>()
            .ForCtorParam(
                nameof(CreateTourCategoryResponseDto.Name),
                opt => opt.MapFrom(src => src.Name)
            );

        CreateMap<Tour, GetTourResponseDto>()
            .ForCtorParam(nameof(GetTourResponseDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(GetTourResponseDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(
                nameof(GetTourResponseDto.Description),
                opt => opt.MapFrom(src => src.Description)
            )
            .ForCtorParam(nameof(GetTourResponseDto.Price), opt => opt.MapFrom(src => src.Price))
            .ForCtorParam(
                nameof(GetTourResponseDto.Location),
                opt => opt.MapFrom(src => src.Location)
            )
            .ForCtorParam(
                nameof(GetTourResponseDto.GoogleMapsLocation),
                opt => opt.MapFrom(src => src.GoogleMapsLocation)
            )
            .ForCtorParam(
                nameof(GetTourResponseDto.MaxCapacity),
                opt => opt.MapFrom(src => src.MaxCapacity)
            )
            .ForCtorParam(nameof(GetTourResponseDto.Images), opt => opt.MapFrom(src => src.Images))
            .ForCtorParam(
                nameof(GetTourResponseDto.Sessions),
                opt => opt.MapFrom(src => src.Sessions)
            )
            .ForCtorParam(
                nameof(GetTourResponseDto.TourCategories),
                opt => opt.MapFrom(src => src.Categories)
            );
    }
}
