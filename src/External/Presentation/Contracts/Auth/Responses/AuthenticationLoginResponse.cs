namespace Presentation.Contracts.Auth.Responses;

public record AuthenticationLoginResponse(string Token, string Id, string Role);
