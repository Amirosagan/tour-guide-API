using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Common;
using Application.Authentication.Queries.UserLogin;
using AutoMapper;
using Presentation.Contracts.Auth.Requests;
using Presentation.Contracts.Auth.Responses;

namespace Presentation.Profiles;

public class AuthProfiles : Profile
{
    public AuthProfiles()
    {
        CreateMap<UserRegisterCommand, AuthenticationSignupRequest>();
        CreateMap<UserLoginQuery, AuthenticationLoginRequest>();
        
        CreateMap<AuthenticationResponse, AuthenticationSignupResponse>();
        CreateMap<AuthenticationResponse, AuthenticationLoginResponse>();
    }
    
}