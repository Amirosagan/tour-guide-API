using Application.Interfaces;

namespace Infrastructure.EmailService;

public class EmailTemplateService : IEmailTemplateService
{
    private readonly string _templatePath;
/// <summary>
/// Initializes a new instance of the <see cref="EmailTemplateService"/> class.
/// Sets the template path for loading email templates. If HTML files are present
/// in the current directory, it uses the current directory as the template path.
/// Otherwise, it defaults to a specified path relative to the current directory.
/// </summary>
    public EmailTemplateService()
    {
        if (Directory.GetFiles(Directory.GetCurrentDirectory(), "*.html").Length > 0)
        {
            _templatePath = Directory.GetCurrentDirectory();
        }
        else
        {
            _templatePath = Path.Combine(Directory.GetCurrentDirectory() + "/Templates");
        }
    }
    /// <summary>
    /// Loads the specified template from the template path and replaces
    /// placeholders in the template with the given replacement values.
    /// </summary>
    /// <param name="templateName">The name of the template to load.</param>
    /// <param name="placeholders">A dictionary of placeholder names to replacement values.</param>
    /// <returns>The loaded template string with placeholders replaced.</returns>
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