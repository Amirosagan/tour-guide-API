namespace Domain.Email;

public static class EmailPlaceHolders
{
    public static class WelcomeEmailPlaceholders
    {
        public const string CustomerName = "{{CustomerName}}";
        public const string Otp = "{{OTP}}";
    }

    public static class ForgotPasswordPlaceholders
    {
        public const string CustomerName = "{{CustomerName}}";
        public const string Otp = "{{OTP}}";
    }
}
