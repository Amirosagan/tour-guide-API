namespace Application.Interfaces;

public interface IEmailTemplateService
{
    public string LoadTemplate(string templateName, Dictionary<String, String> placeholders);
}
