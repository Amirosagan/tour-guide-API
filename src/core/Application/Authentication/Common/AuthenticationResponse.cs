namespace Application.Authentication.Common;

public record AuthenticationResponse(
    string Token,
    string Id,
    string Role
    );