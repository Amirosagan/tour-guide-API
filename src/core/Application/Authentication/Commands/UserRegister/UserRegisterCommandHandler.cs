using Application.Authentication.Common;
using Application.Interfaces;
using Domain.Enums;
using Domain.Errors;
using Domain.Identity;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Authentication.Commands.UserRegister;

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ErrorOr<UserRegisterCommandResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IEmailServiceSender _emailServiceSender;

    public UserRegisterCommandHandler(UserManager<NormalUser> normalUserManager, IJwtTokenGenerator jwtTokenGenerator, IEmailServiceSender emailServiceSender)
    {
        _normalUserManager = normalUserManager;
        _jwtTokenGenerator = jwtTokenGenerator;
        _emailServiceSender = emailServiceSender;
    }

    public async Task<ErrorOr<UserRegisterCommandResponse>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
    {
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
            return DomainErrors.UserRegister.DuplicateEmail(email: request.Email);
        }
        
        var phoneNumberExist = await _normalUserManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
        
        if (phoneNumberExist != null)
        {
            return DomainErrors.UserRegister.DuplicatePhoneNumber(phoneNumber: request.PhoneNumber);
        }
        
        var result = await _normalUserManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return Error.Unexpected(description: result.Errors.Select(x => x.Description).Aggregate((x, y) => $"{x}, {y}"));
        }

        await _normalUserManager.AddToRoleAsync(user, Roles.NormalUser.ToString());

        var code = await _normalUserManager.GenerateEmailConfirmationTokenAsync(user);
        
        await _emailServiceSender.SendEmailAsync(user.Email, "Confirm your email", $"Please confirm your account by clicking this link: https://localhost:5001/Auth/ConfirmEmail?email={user.Email}&token={code}");
        
        return new UserRegisterCommandResponse();
    }
}