using Application.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Infrastructure.EmailService;

public class EmailServiceSender : IEmailServiceSender
{
    private readonly EmailSettings _emailSettings;

    public EmailServiceSender(EmailSettings emailSettings)
    {
        _emailSettings = emailSettings;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _emailSettings.SmtpServer,
            _emailSettings.Port,
            SecureSocketOptions.StartTls
        );
        await smtp.AuthenticateAsync(_emailSettings.Email, _emailSettings.Password);
        await smtp.SendAsync(CreateEmailMessage(email, subject, message));
        await smtp.DisconnectAsync(true);
    }

    private MimeMessage CreateEmailMessage(string email, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(_emailSettings.Email));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
        return emailMessage;
    }
}
