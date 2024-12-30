using Application.Authentication.Common;
using Application.Authentication.Queries.UserLogin;
using Application.Interfaces;
using Domain.Enums;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Quieres.UserLogin;

public class UserLoginQueryHandler : IRequestHandler<UserLoginQuery, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManger;
    private readonly UserManager<TourGuide> _tourGuideManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly ILogger<UserLoginQueryHandler> _logger;

    public UserLoginQueryHandler(UserManager<NormalUser> normalUserManger, UserManager<TourGuide> tourGuideManager, IJwtTokenGenerator jwtTokenGenerator, ILogger<UserLoginQueryHandler> logger)
    {
        _normalUserManger = normalUserManger;
        _tourGuideManager = tourGuideManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _logger = logger;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(UserLoginQuery request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Logging in user with email {Email}", request.Email);
        var normalUser = await _normalUserManger.FindByEmailAsync(request.Email);
        if (normalUser != null)
        {
            if (!normalUser.EmailConfirmed)
            {
                _logger.LogWarning("User with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await _normalUserManger.CheckPasswordAsync(normalUser, request.Password);
            if (!result)
            {
                _logger.LogWarning("Invalid credentials for user with email {Email}", request.Email);
                return DomainErrors.UserLogin.InvalidCredentials();
            }
            
            var jwtToken = _jwtTokenGenerator.GenerateToken(normalUser, Roles.NormalUser.ToString());
            _logger.LogInformation("User with email {Email} logged in", request.Email);
            return new AuthenticationResponse(jwtToken, normalUser.Id, Roles.NormalUser.ToString());
        }
        var tourGuidUser = await _tourGuideManager.FindByEmailAsync(request.Email);
        if (tourGuidUser != null)
        {
            if (tourGuidUser.EmailConfirmed)
            {
                _logger.LogWarning("Tour guide with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await _tourGuideManager.CheckPasswordAsync(tourGuidUser, request.Password);
            if (!result)
            {
                _logger.LogWarning("Invalid credentials for tour guide with email {Email}", request.Email);
                return DomainErrors.UserLogin.InvalidCredentials();
            }
            
            var jwtToken = _jwtTokenGenerator.GenerateToken(tourGuidUser, Roles.TourGuide.ToString());
            _logger.LogInformation("Tour guide with email {Email} logged in", request.Email);
            return new AuthenticationResponse(jwtToken, tourGuidUser.Id, Roles.TourGuide.ToString());
        }
        
        _logger.LogWarning("User with email {Email} not found", request.Email);
        return DomainErrors.UserLogin.InvalidCredentials();
    }
}