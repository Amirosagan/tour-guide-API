using Application.Interfaces;

namespace Infrastructure.EmailService;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly string _templatePath;
    public EmailTemplateService()
    {
        _templatePath = Path.Combine(Directory.GetCurrentDirectory() + "../../core/Domain/Email/Templates");
        Console.WriteLine(_templatePath);
    }

    public string LoadTemplate(string templateName, Dictionary<string, string> placeholders)
    {
        var templatePath = Path.Combine(_templatePath, templateName + ".html");
        var template = File.ReadAllText(templatePath);
        foreach (var placeholder in placeholders)
        {
            template = template.Replace(placeholder.Key, placeholder.Value);
        }
        return template;
    }
}