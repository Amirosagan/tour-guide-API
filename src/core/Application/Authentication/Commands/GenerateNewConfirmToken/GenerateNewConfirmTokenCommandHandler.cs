using Application.Interfaces;
using Domain.Errors;
using Domain.Identity;
using MediatR;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.GenerateNewConfirmToken;

public class GenerateNewConfirmTokenCommandHandler : IRequestHandler<GenerateNewConfirmTokenCommand, ErrorOr<GenerateNewConfirmTokenCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IEmailServiceSender _emailServiceSender;

    public GenerateNewConfirmTokenCommandHandler(UserManager<NormalUser> normalUserManager, IEmailServiceSender emailServiceSender)
    {
        _normalUserManager = normalUserManager;
        _emailServiceSender = emailServiceSender;
    }

    public async Task<ErrorOr<GenerateNewConfirmTokenCommandResponse>> Handle(GenerateNewConfirmTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _normalUserManager.FindByEmailAsync(request.Email);
        
        if (user == null)
        {
            return DomainErrors.TokenConfirmation.EmailNotFound();
        }
        
        if(user.EmailConfirmed)
        {
            return DomainErrors.TokenConfirmation.EmailAlreadyVerified();
        }
        
        var token = await _normalUserManager.GenerateEmailConfirmationTokenAsync(user);
        
        await _emailServiceSender.SendEmailAsync(request.Email, "Confirm your email", $"Please confirm your account by clicking this link: https://localhost:5001/Auth/ConfirmEmail?email={user.Email}&token={token}");
        
        return new GenerateNewConfirmTokenCommandResponse();
    }
}