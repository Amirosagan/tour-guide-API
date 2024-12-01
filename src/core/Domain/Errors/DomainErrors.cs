using ErrorOr;

namespace Domain.Errors;

public static class DomainErrors
{
    public static class UserRegister
    {
        public static Error DuplicateEmail(string email) => Error.Conflict(description: $"Email {email} is already taken.");
        public static Error DuplicatePhoneNumber(string phoneNumber) => Error.Conflict(description: $"Phone number {phoneNumber} is already taken.");
        public static Error ServerError() => Error.Failure(description: "An error occurred while registering the user.");
    }
    public static class UserLogin
    {
        public static Error InvalidCredentials() => Error.Unauthorized(description: "Invalid email or password.");
        public static Error EmailNotConfirmed() => Error.Unauthorized(description: "Email has not been confirmed.");
    }
    public static class TokenConfirmation
    {
        public static Error InvalidToken() => Error.Failure(description: "Invalid token.");
        public static Error TokenExpired() => Error.Failure(description: "Token has expired.");
        public static Error TokenAlreadyUsed() => Error.Failure(description: "Token has already been used.");
        public static Error TokenNotFound() => Error.NotFound(description: "Token not found.");
        public static Error EmailAlreadyVerified() => Error.Conflict(description: "Email has already been verified.");
        public static Error EmailNotFound() => Error.NotFound(description: "Email not found.");
    }
}