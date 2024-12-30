using Application.Authentication.Common;
using Application.Interfaces;
using Domain.Email;
using Domain.Enums;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.UserRegister;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ErrorOr<UserRegisterCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IEmailServiceSender _emailServiceSender;
    private readonly ILogger<UserRegisterCommandHandler> _logger;
    private readonly IOtpGenerator _otpGenerator;
    private readonly IEmailTemplateService _emailTemplateService;
    
    public UserRegisterCommandHandler(UserManager<NormalUser> normalUserManager, IEmailServiceSender emailServiceSender, ILogger<UserRegisterCommandHandler> logger, IOtpGenerator otpGenerator, IEmailTemplateService emailTemplateService)
    {
        _normalUserManager = normalUserManager;
        _emailServiceSender = emailServiceSender;
        _logger = logger;
        _otpGenerator = otpGenerator;
        _emailTemplateService = emailTemplateService;
    }

    public async Task<ErrorOr<UserRegisterCommandResponse>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Registering user with email {Email}", request.Email);
        var user = new NormalUser
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            OAuthUser = false,
            ProfilePictureUrl = request.ProfilePictureUrl
        };
        
        var emailExist = await _normalUserManager.FindByEmailAsync(request.Email);
        
        if (emailExist != null)
        {
            _logger.LogWarning("User with email {Email} already exists", request.Email);
            return DomainErrors.UserRegister.DuplicateEmail(email: request.Email);
        }
        
        var phoneNumberExist = await _normalUserManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
        
        if (phoneNumberExist != null)
        {
            _logger.LogWarning("User with phone number {PhoneNumber} already exists", request.PhoneNumber);
            return DomainErrors.UserRegister.DuplicatePhoneNumber(phoneNumber: request.PhoneNumber);
        }
        
        var result = await _normalUserManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            _logger.LogError("Error registering user with email {Email}", request.Email);
            return Error.Unexpected(description: result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
        }

        await _normalUserManager.AddToRoleAsync(user, Roles.NormalUser.ToString());
        
        var token = await _normalUserManager.GenerateEmailConfirmationTokenAsync(user);

        var otp = _otpGenerator.GenerateOtp(token);

        var placeHolders = new Dictionary<string, string>
        {
            { EmailPlaceHolders.WelcomeEmailPlaceholders.CustomerName, user.FullName },
            { EmailPlaceHolders.WelcomeEmailPlaceholders.Otp, otp }
        };
        
        var emailBody = _emailTemplateService.LoadTemplate("WelcomeEmail", placeHolders);
        var emailSubject = "Welcome to our platform! LOCO";
        
        await _emailServiceSender.SendEmailAsync(user.Email, emailSubject, emailBody);
        
        _logger.LogInformation("User with email {Email} registered", request.Email);
        return new UserRegisterCommandResponse();
    }
}