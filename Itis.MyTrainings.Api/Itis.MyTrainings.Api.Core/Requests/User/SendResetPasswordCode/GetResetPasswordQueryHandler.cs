using System.Net.Mail;
using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Exceptions;
using Itis.MyTrainings.Api.Core.Managers;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;

/// <summary>
/// Обработчик запроса <see cref="SendResetPasswordQuery"/>
/// </summary>
public class GetResetPasswordQueryHandler:
    IRequestHandler<SendResetPasswordQuery, SendResetPasswordCodeResponse>
{
    private IUserService _userService;
    private IEmailSenderService _emailSenderService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="emailSenderService">Сервис для работы с Email</param>
    public GetResetPasswordQueryHandler(
        IUserService userService,
        IEmailSenderService emailSenderService)
    {
        _userService = userService;
        _emailSenderService = emailSenderService;
    }
    
    /// <inheritdoc />
    public async Task<SendResetPasswordCodeResponse> Handle(SendResetPasswordQuery request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByEmailAsync(request.Email)
            ?? throw new EntityNotFoundException<Entities.User>($"Не найдены пользователи со следующим email: {request.Email}");

        var token = await _userService.GetPasswordResetTokenAsync(user);

        var body = HtmlFileManager.GetHtmlFileBody(
            TemplatePaths.TemplatePath, 
            TemplatePaths.ResetPasswordTemplate); 
        
        var subject = EmailMessageSubjects.ResetPassword;
        
        var placeholders = new Dictionary<string, string>()
        {
            { "{code}", token },
        };

        string result;
        try
        {
            await _emailSenderService.SendMessageAsync(subject, body, request.Email, placeholders, cancellationToken);
            result = "Сообщение отправлено";
        }
        catch (SmtpFailedRecipientsException e)
        {
            result = e.Message;
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("Сообщение не может быть пустым");
            throw;
        }

        return new SendResetPasswordCodeResponse(result);
    }
}