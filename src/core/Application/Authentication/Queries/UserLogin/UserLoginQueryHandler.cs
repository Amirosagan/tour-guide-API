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

public class UserLoginQueryHandler(
    UserManager<NormalUser> normalUserManger,
    UserManager<TourGuide> tourGuideManager,
    IJwtTokenGenerator jwtTokenGenerator,
    ILogger<UserLoginQueryHandler> logger
) : IRequestHandler<UserLoginQuery, ErrorOr<AuthenticationResponse>>
{
    public async Task<ErrorOr<AuthenticationResponse>> Handle(
        UserLoginQuery request,
        CancellationToken cancellationToken
    )
    {
        logger.LogInformation("Logging in user with email {Email}", request.Email);
        var normalUser = await normalUserManger.FindByEmailAsync(request.Email);
        if (normalUser != null)
        {
            if (!normalUser.EmailConfirmed)
            {
                logger.LogWarning("User with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await normalUserManger.CheckPasswordAsync(normalUser, request.Password);
            if (!result)
            {
                logger.LogWarning("Invalid credentials for user with email {Email}", request.Email);
                return DomainErrors.UserLogin.InvalidCredentials();
            }

            var jwtToken = jwtTokenGenerator.GenerateToken(normalUser, Roles.NormalUser.ToString());
            logger.LogInformation("User with email {Email} logged in", request.Email);
            return new AuthenticationResponse(jwtToken, normalUser.Id, Roles.NormalUser.ToString());
        }
        var tourGuidUser = await tourGuideManager.FindByEmailAsync(request.Email);
        if (tourGuidUser != null)
        {
            if (tourGuidUser.EmailConfirmed)
            {
                logger.LogWarning("Tour guide with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var result = await tourGuideManager.CheckPasswordAsync(tourGuidUser, request.Password);
            if (!result)
            {
                logger.LogWarning(
                    "Invalid credentials for tour guide with email {Email}",
                    request.Email
                );
                return DomainErrors.UserLogin.InvalidCredentials();
            }

            var jwtToken = jwtTokenGenerator.GenerateToken(
                tourGuidUser,
                Roles.TourGuide.ToString()
            );
            logger.LogInformation("Tour guide with email {Email} logged in", request.Email);
            return new AuthenticationResponse(
                jwtToken,
                tourGuidUser.Id,
                Roles.TourGuide.ToString()
            );
        }

        logger.LogWarning("User with email {Email} not found", request.Email);
        return DomainErrors.UserLogin.InvalidCredentials();
    }
}
