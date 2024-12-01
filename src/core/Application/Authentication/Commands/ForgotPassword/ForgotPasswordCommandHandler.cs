using Application.Interfaces;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, ErrorOr<ForgotPasswordCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly UserManager<TourGuide> _tourGuideManager;
    private readonly IEmailServiceSender _emailServiceSender;

    public ForgotPasswordCommandHandler(UserManager<NormalUser> normalUserManager, UserManager<TourGuide> tourGuideManager, IEmailServiceSender emailServiceSender)
    {
        _normalUserManager = normalUserManager;
        _tourGuideManager = tourGuideManager;
        _emailServiceSender = emailServiceSender;
    }

    public async Task<ErrorOr<ForgotPasswordCommandResponse>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _normalUserManager.FindByEmailAsync(request.Email);
        if (user != null)
        {
            if (!user.EmailConfirmed)
            {
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var token = await _normalUserManager.GeneratePasswordResetTokenAsync(user);
            await _emailServiceSender.SendEmailAsync(request.Email, "Reset your password", $"Please reset your password by clicking this link: https://localhost:5162/Auth/ResetPassword?email={user.Email}&token={token}");
            return new ForgotPasswordCommandResponse();
        }
        var tourGuide = await _tourGuideManager.FindByEmailAsync(request.Email);
        if(tourGuide != null)
        {
            if (!tourGuide.EmailConfirmed)
            {
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            var token = await _tourGuideManager.GeneratePasswordResetTokenAsync(tourGuide);
            await _emailServiceSender.SendEmailAsync(request.Email, "Reset your password", $"Please reset your password by clicking this link: https://localhost:5162/Auth/ResetPassword?email={tourGuide.Email}&token={token}");
            return new ForgotPasswordCommandResponse();
        }

        return DomainErrors.TokenConfirmation.EmailNotFound();
    }
}