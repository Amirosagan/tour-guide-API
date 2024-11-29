using Application.Authentication.Common;
using AutoMapper;
using Presentation.Contracts.Auth.Responses;

namespace Presentation.Profiles;

public class AuthProfiles : Profile
{
    public AuthProfiles()
    {
        CreateMap<AuthenticationResponse, AuthenticationSignupResponse>();
    }
    
}