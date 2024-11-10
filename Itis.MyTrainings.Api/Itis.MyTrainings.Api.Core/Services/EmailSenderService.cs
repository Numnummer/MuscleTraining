using System.Net;
using System.Net.Mail;
using Itis.MyTrainings.Api.Core.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Services;

/// <inheritdoc />
public class EmailSenderService: IEmailSenderService
{
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    /// <inheritdoc />
    public async Task SendMessageAsync(string? subject, string body, string sendTo, Dictionary<string, string>? placeholders, CancellationToken cancellationToken)
    {
        if (placeholders != null)
            foreach (var (placeholder, value) in placeholders)
                body = body.Replace(placeholder, value);
        
        var from = new MailAddress(_configuration["EmailConfig:Sender"]!,
            _configuration["EmailConfig:SenderName"]!);
        var to = new MailAddress(sendTo);
        var message = new MailMessage(from, to)
        {
            Subject = subject ?? default,
            Body = body,
            IsBodyHtml = true
        };

        var smtp = new SmtpClient(_configuration["SmtpSettings:SmtpAddress"],
            int.Parse(_configuration["SmtpSettings:Port"]!))
        {
            Credentials = new NetworkCredential(_configuration["EmailConfig:Sender"]!,
                _configuration["EmailConfig:SenderPassword"]!),
            EnableSsl = true,
            UseDefaultCredentials = false,
        };
        
        await smtp.SendMailAsync(message, cancellationToken);
    }
}