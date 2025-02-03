using Application.Interfaces;
using Application.Interfaces.UnitOfWork;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler
    : IRequestHandler<ConfirmEmailCommand, ErrorOr<ConfirmEmailCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ConfirmEmailCommandHandler> _logger;
    private readonly IOtpGenerator _otpGenerator;

    public ConfirmEmailCommandHandler(
        UserManager<NormalUser> normalUserManager,
        ILogger<ConfirmEmailCommandHandler> logger,
        IOtpGenerator otpGenerator,
        IUserRepository userRepository
    )
    {
        _normalUserManager = normalUserManager;
        _logger = logger;
        _otpGenerator = otpGenerator;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<ConfirmEmailCommandResponse>> Handle(
        ConfirmEmailCommand request,
        CancellationToken cancellationToken
    )
    {
        _logger.LogInformation("Confirming email for user with email {Email}", request.Email);
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

        var token = _otpGenerator.VerifyOtp(request.Otp);
        var result = await _normalUserManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
        {
            _logger.LogWarning("Otp for user with email {Email} is invalid", request.Email);
            return DomainErrors.TokenConfirmation.InvalidToken();
        }

        await _userRepository.ConfirmEmail(request.Email);

        _logger.LogInformation("Email confirmed for user with email {Email}", request.Email);
        return new ConfirmEmailCommandResponse();
    }
}
