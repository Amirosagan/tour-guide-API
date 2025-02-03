using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommand(string Email)
    : IRequest<ErrorOr<ForgotPasswordCommandResponse>>;
