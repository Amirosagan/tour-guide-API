using Application.Authentication.Commands.ConfirmEmail;
using Application.Authentication.Commands.ForgotPassword;
using Application.Authentication.Commands.GenerateNewConfirmToken;
using Application.Authentication.Commands.UserRegister;
using Application.Authentication.Common;
using Application.Authentication.Queries.ResetPassword;
using Application.Authentication.Queries.UserLogin;
using AutoMapper;
using Presentation.Contracts.Auth.Requests;
using Presentation.Contracts.Auth.Responses;

namespace Presentation.Profiles;

public class AuthProfiles : Profile
{
    public AuthProfiles()
    {
        CreateMap<AuthenticationSignupRequest, UserRegisterCommand>();
        CreateMap<AuthenticationLoginRequest, UserLoginQuery>();
        CreateMap<NewEmailConfirmLinkRequest, GenerateNewConfirmTokenCommand>();
        CreateMap<ConfirmEmailRequest, ConfirmEmailCommand>();
        CreateMap<ForgotPasswordRequest, ForgotPasswordCommand>();
        CreateMap<ResetPasswordRequest, ResetPasswordQuery>();

        CreateMap<UserRegisterCommandResponse, AuthenticationSignupResponse>();
        CreateMap<AuthenticationResponse, AuthenticationLoginResponse>();
        CreateMap<GenerateNewConfirmTokenCommandResponse, NewEmailConfirmLinkResponse>();
        CreateMap<ConfirmEmailCommandResponse, ConfirmEmailResponse>();
        CreateMap<ForgotPasswordCommandResponse, ForgotPasswordResponse>();
        CreateMap<ResetPasswordQueryResponse, ResetPasswordResponse>();
    }
}
