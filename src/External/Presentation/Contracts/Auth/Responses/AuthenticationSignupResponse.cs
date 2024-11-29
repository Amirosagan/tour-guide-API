namespace Presentation.Contracts.Auth.Responses;

public record AuthenticationSignupResponse(
    string Token,
    string Id,
    string Role
    );