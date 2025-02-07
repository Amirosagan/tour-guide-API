using Application.Tours.Commands.CreateTour;
using Application.Tours.Helpers;
using Application.Tours.Queries.GetAllTours;

using AutoMapper;

using Domain.Entities;

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
    }

    private void GetAllToursProfile()
    {
        CreateMap<GetAllToursRequestDto, GetAllToursQuery>();

        CreateMap<Tour, TourInListDto>()
            .ForCtorParam(nameof(TourInListDto.Id), opt => opt.MapFrom(src => src.Id))
            .ForCtorParam(nameof(TourInListDto.Name), opt => opt.MapFrom(src => src.Name))
            .ForCtorParam(nameof(TourInListDto.Price), opt => opt.MapFrom(src => src.Price))
            .ForCtorParam(nameof(TourInListDto.Location), opt => opt.MapFrom(src => src.Location))
            .ForCtorParam(nameof(TourInListDto.Image), opt => opt.MapFrom(src => src.Images.FirstOrDefault()));
        
        CreateMap<GetAllToursQueryResponse, GetAllToursResponseDto>();
    }
}
