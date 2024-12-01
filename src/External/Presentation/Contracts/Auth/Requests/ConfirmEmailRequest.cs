namespace Presentation.Contracts.Auth.Requests;

public record ConfirmEmailRequest(
    string Email,
    string Token
    );