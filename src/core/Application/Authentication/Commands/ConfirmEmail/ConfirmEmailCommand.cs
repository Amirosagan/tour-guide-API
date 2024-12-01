using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ConfirmEmail;

public record ConfirmEmailCommand(
    string Email,
    string Token
    ) : IRequest<ErrorOr<ConfirmEmailCommandResponse>>;