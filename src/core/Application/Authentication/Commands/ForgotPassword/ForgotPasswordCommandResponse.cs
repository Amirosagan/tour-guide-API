namespace Application.Authentication.Commands.ForgotPassword;

public record ForgotPasswordCommandResponse(
    string Message = "Please check your email to reset your password."
);
