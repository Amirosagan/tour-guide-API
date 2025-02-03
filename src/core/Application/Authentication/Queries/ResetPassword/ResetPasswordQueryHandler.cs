using Application.Interfaces;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Queries.ResetPassword;

public class ResetPasswordQueryHandler
    : IRequestHandler<ResetPasswordQuery, ErrorOr<ResetPasswordQueryResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly UserManager<TourGuide> _tourGuideManager;
    private readonly ILogger<ResetPasswordQueryHandler> _logger;
    private readonly IOtpGenerator _otpGenerator;

    public ResetPasswordQueryHandler(
        UserManager<NormalUser> normalUserManager,
        UserManager<TourGuide> tourGuideManager,
        ILogger<ResetPasswordQueryHandler> logger,
        IOtpGenerator otpGenerator
    )
    {
        _normalUserManager = normalUserManager;
        _tourGuideManager = tourGuideManager;
        _logger = logger;
        _otpGenerator = otpGenerator;
    }

    public async Task<ErrorOr<ResetPasswordQueryResponse>> Handle(
        ResetPasswordQuery request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Resetting password for user with email {Email}", request.Email);
        var user = await _normalUserManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            var token = _otpGenerator.VerifyOtp(request.Otp);
            var result = await _normalUserManager.ResetPasswordAsync(
                user,
                token,
                request.NewPassword
            );

            if (result.Errors.Any())
            {
                _logger.LogError(
                    "Error resetting password for user with email {Email}",
                    request.Email
                );
                return Error.Failure(
                    description: result
                        .Errors.Select(x => x.Description)
                        .Aggregate((x, y) => $"{x}, {y}")
                );
            }

            return new ResetPasswordQueryResponse();
        }

        var tourGuide = await _tourGuideManager.FindByEmailAsync(request.Email);

        if (tourGuide != null)
        {
            var token = _otpGenerator.VerifyOtp(request.Otp);
            var result = await _tourGuideManager.ResetPasswordAsync(
                tourGuide,
                token,
                request.NewPassword
            );

            if (result.Errors.Any())
            {
                _logger.LogError(
                    "Error resetting password for tour guide with email {Email}",
                    request.Email
                );
                return Error.Failure(
                    description: result
                        .Errors.Select(x => x.Description)
                        .Aggregate((x, y) => $"{x}, {y}")
                );
            }

            _logger.LogInformation(
                "Password reset for tour guide with email {Email}",
                request.Email
            );
            return new ResetPasswordQueryResponse();
        }

        _logger.LogWarning("User with email {Email} not found", request.Email);
        return DomainErrors.TokenConfirmation.EmailNotFound();
    }
}
