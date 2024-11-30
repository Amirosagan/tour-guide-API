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

public class UserRegisterCommandHandler : IRequestHandler<UserRegisterCommand, ErrorOr<AuthenticationResponse>>
{
    private readonly UserManager<NormalUser> _normalUserManager;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserRegisterCommandHandler(UserManager<NormalUser> normalUserManager, IJwtTokenGenerator jwtTokenGenerator)
    {
        _normalUserManager = normalUserManager;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<ErrorOr<AuthenticationResponse>> Handle(UserRegisterCommand request, CancellationToken cancellationToken)
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

        var jwtToken = _jwtTokenGenerator.GenerateToken(user, Roles.NormalUser.ToString());
        
        return new AuthenticationResponse(jwtToken, user.Id, Roles.NormalUser.ToString());
    }
}