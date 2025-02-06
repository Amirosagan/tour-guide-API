using Application.Tours.Commands.CreateTour;
using Application.Tours.Helpers;
using AutoMapper;
using Presentation.Contracts.Tours.Common;
using Presentation.Contracts.Tours.Requests;

namespace Presentation.Profiles;

public class TourProfile : Profile
{
    public TourProfile()
    {
        CreateMap<CreateTourDto, CreateTourCommand>();

        CreateMap<SessionDto, SessionInRecords>();
    }
}
