namespace Application.Authentication.Commands.GenerateNewConfirmToken;

public record GenerateNewConfirmTokenCommandResponse(
    string Message = "Please check your email to verify your account."
);
