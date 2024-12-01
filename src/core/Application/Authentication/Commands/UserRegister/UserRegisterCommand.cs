using Application.Authentication.Common;
using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.UserRegister;

public record UserRegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? ProfilePictureUrl
    ) : IRequest<ErrorOr<UserRegisterCommandResponse>>;