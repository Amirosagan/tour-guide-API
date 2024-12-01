namespace Application.Authentication.Commands.UserRegister;

public record UserRegisterCommandResponse(
    string Message = "Please check your email to verify your account."
    );