namespace Presentation.Contracts.Auth.Requests;

public record ResetPasswordRequest(
    string Email,
    string Otp,
    string NewPassword
    );