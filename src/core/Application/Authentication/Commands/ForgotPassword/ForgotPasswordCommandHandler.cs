using Application.Interfaces;
using Domain.Email;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, ErrorOr<ForgotPasswordCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly UserManager<TourGuide> _tourGuideManager;
    private readonly IEmailServiceSender _emailServiceSender;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly ILogger<ForgotPasswordCommandHandler> _logger;
    private readonly IOtpGenerator _otpGenerator;

    public ForgotPasswordCommandHandler(
        UserManager<NormalUser> normalUserManager,
        UserManager<TourGuide> tourGuideManager,
        IEmailServiceSender emailServiceSender,
        ILogger<ForgotPasswordCommandHandler> logger,
        IEmailTemplateService emailTemplateService,
        IOtpGenerator otpGenerator
    )
    {
        _normalUserManager = normalUserManager;
        _tourGuideManager = tourGuideManager;
        _emailServiceSender = emailServiceSender;
        _logger = logger;
        _emailTemplateService = emailTemplateService;
        _otpGenerator = otpGenerator;
    }

    public async Task<ErrorOr<ForgotPasswordCommandResponse>> Handle(
        ForgotPasswordCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Forgot password for user with email {Email}", request.Email);
        var user = await _normalUserManager.FindByEmailAsync(request.Email);
        const string subject = "Reset your password";
        if (user != null)
        {
            if (!user.EmailConfirmed)
            {
                _logger.LogWarning("User with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            _logger.LogInformation("Sending email to user with email {Email}", request.Email);
            var token = await _normalUserManager.GeneratePasswordResetTokenAsync(user);
            var otp = _otpGenerator.GenerateOtp(token);
            var placeholders = new Dictionary<String, String>()
            {
                { EmailPlaceHolders.ForgotPasswordPlaceholders.Otp, otp },
                { EmailPlaceHolders.ForgotPasswordPlaceholders.CustomerName, user.FullName },
            };
            var body = _emailTemplateService.LoadTemplate("ForgotPasswordOtpEmail", placeholders);
            await _emailServiceSender.SendEmailAsync(request.Email, subject, body);

            return new ForgotPasswordCommandResponse();
        }
        var tourGuide = await _tourGuideManager.FindByEmailAsync(request.Email);
        if (tourGuide != null)
        {
            if (!tourGuide.EmailConfirmed)
            {
                _logger.LogWarning("Tour guide with email {Email} not confirmed", request.Email);
                return DomainErrors.UserLogin.EmailNotConfirmed();
            }
            _logger.LogInformation("Sending email to tour guide with email {Email}", request.Email);
            var token = await _tourGuideManager.GeneratePasswordResetTokenAsync(tourGuide);
            var otp = _otpGenerator.GenerateOtp(token);
            var placeholders = new Dictionary<String, String>()
            {
                { EmailPlaceHolders.ForgotPasswordPlaceholders.Otp, otp },
                { EmailPlaceHolders.ForgotPasswordPlaceholders.CustomerName, user.FullName },
            };
            var body = _emailTemplateService.LoadTemplate("ForgotPasswordOtpEmail", placeholders);
            await _emailServiceSender.SendEmailAsync(request.Email, subject, body);
            return new ForgotPasswordCommandResponse();
        }

        _logger.LogWarning("User with email {Email} not found", request.Email);
        return DomainErrors.TokenConfirmation.EmailNotFound();
    }
}
