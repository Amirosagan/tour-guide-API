namespace Application.Interfaces;

public interface IEmailServiceSender
{
    Task SendEmailAsync(string email, string subject, string message);
}