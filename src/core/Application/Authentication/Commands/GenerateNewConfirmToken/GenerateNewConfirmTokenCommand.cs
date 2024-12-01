using ErrorOr;
using MediatR;

namespace Application.Authentication.Commands.GenerateNewConfirmToken;

public record GenerateNewConfirmTokenCommand(
    string Email
    ) : IRequest<ErrorOr<GenerateNewConfirmTokenCommandResponse>>;
