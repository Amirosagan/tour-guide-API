using Application.Authentication.Common;
using Application.Authentication.Queries.UserLogin;
using Application.Interfaces;
using Domain.Enums;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Quieres.UserLogin;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManger;
    private readonly UserManager<TourGuide> _tourGuideManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserLoginQueryHandler(UserManager<NormalUser> normalUserManger, UserManager<TourGuide> tourGuideManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _normalUserManger = normalUserManger;
        _tourGuideManager = tourGuideManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        var normalUser = await _normalUserManger.FindByEmailAsync(request.Email);
        if (normalUser != null)
        {
            if (!normalUser.EmailConfirmed)
            {
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await _normalUserManger.CheckPasswordAsync(normalUser, request.Password);
            if (!result)
            {
                return DomainErrors.UserLogin.InvalidCredentials();
            }
            
            var jwtToken = _jwtTokenGenerator.GenerateToken(normalUser, Roles.NormalUser.ToString());
            return new AuthenticationResponse(jwtToken, normalUser.Id, Roles.NormalUser.ToString());
        }
        var tourGuidUser = await _tourGuideManager.FindByEmailAsync(request.Email);
        if (tourGuidUser != null)
        {
            if (tourGuidUser.EmailConfirmed)
            {
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await _tourGuideManager.CheckPasswordAsync(tourGuidUser, request.Password);
            if (!result)
            {
                return DomainErrors.UserLogin.InvalidCredentials();
            }
            
            var jwtToken = _jwtTokenGenerator.GenerateToken(tourGuidUser, Roles.TourGuide.ToString());
            return new AuthenticationResponse(jwtToken, tourGuidUser.Id, Roles.TourGuide.ToString());
        }
        
        return DomainErrors.UserLogin.InvalidCredentials();
    }
}