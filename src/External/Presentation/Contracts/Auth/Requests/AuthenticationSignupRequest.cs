namespace Presentation.Contracts.Auth.Requests;

public record AuthenticationSignupRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string PhoneNumber,
    string? ProfilePictureUrl
    );