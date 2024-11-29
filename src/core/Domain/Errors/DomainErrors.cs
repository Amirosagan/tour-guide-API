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
}