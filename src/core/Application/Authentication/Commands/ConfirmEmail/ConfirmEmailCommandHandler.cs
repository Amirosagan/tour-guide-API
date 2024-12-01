using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Authentication.Commands.ConfirmEmail;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, ErrorOr<ConfirmEmailCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;

    public ConfirmEmailCommandHandler(UserManager<NormalUser> normalUserManager)
    {
        _normalUserManager = normalUserManager;
    }

    public async Task<ErrorOr<ConfirmEmailCommandResponse>> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
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
        
        var result = await _normalUserManager.ConfirmEmailAsync(user, request.Token);
        
        if(result.Errors.Any())
        {
            return  Error.Failure(description: result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
        }
        
        return new ConfirmEmailCommandResponse();
    }
}