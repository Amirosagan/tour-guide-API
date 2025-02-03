using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(string Email, string Otp)
    : IRequest<ErrorOr<ConfirmEmailCommandResponse>>;
