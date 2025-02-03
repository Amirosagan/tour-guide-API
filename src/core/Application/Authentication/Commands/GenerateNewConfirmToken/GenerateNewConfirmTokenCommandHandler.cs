using Application.Interfaces;
using Domain.Email;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.GenerateNewConfirmToken;

public class GenerateNewConfirmTokenCommandHandler
    : IRequestHandler<
        GenerateNewConfirmTokenCommand,
        ErrorOr<GenerateNewConfirmTokenCommandResponse>
    >
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IEmailServiceSender _emailServiceSender;
    private readonly ILogger<GenerateNewConfirmTokenCommandHandler> _logger;
    private readonly IEmailTemplateService _emailTemplateService;
    private readonly IOtpGenerator _otpGenerator;

    public GenerateNewConfirmTokenCommandHandler(
        UserManager<NormalUser> normalUserManager,
        IEmailServiceSender emailServiceSender,
        ILogger<GenerateNewConfirmTokenCommandHandler> logger,
        IEmailTemplateService emailTemplateService,
        IOtpGenerator otpGenerator
    )
    {
        _normalUserManager = normalUserManager;
        _emailServiceSender = emailServiceSender;
        _logger = logger;
        _emailTemplateService = emailTemplateService;
        _otpGenerator = otpGenerator;
    }

    public async Task<ErrorOr<GenerateNewConfirmTokenCommandResponse>> Handle(
        GenerateNewConfirmTokenCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation(
            "Generating new email confirmation token for user with email {Email}",
            request.Email
        );
        var user = await _normalUserManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            _logger.LogWarning("User with email {Email} not found", request.Email);
            return DomainErrors.TokenConfirmation.EmailNotFound();
        }

        if (user.EmailConfirmed)
        {
            _logger.LogWarning("User with email {Email} already verified", request.Email);
            return DomainErrors.TokenConfirmation.EmailAlreadyVerified();
        }

        var token = await _normalUserManager.GenerateEmailConfirmationTokenAsync(user);

        var otp = _otpGenerator.GenerateOtp(token);

        const string subject = "Confirm your email";

        var placeholders = new Dictionary<String, String>()
        {
            { EmailPlaceHolders.WelcomeEmailPlaceholders.Otp, otp },
            { EmailPlaceHolders.WelcomeEmailPlaceholders.CustomerName, user.FullName },
        };

        var body = _emailTemplateService.LoadTemplate("WelcomeEmail", placeholders);

        await _emailServiceSender.SendEmailAsync(request.Email, subject, body);

        _logger.LogInformation(
            "New email confirmation token generated for user with email {Email}",
            request.Email
        );
        return new GenerateNewConfirmTokenCommandResponse();
    }
}
