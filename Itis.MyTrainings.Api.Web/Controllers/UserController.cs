using Itis.MyTrainings.Api.Contracts.Requests.User.GetCurrentUserInfo;
using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithYandex;
using Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Requests.User.CheckUserProfile;
using Itis.MyTrainings.Api.Core.Requests.User.GetCurrentUserInfo;
using Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithYandex;
using Itis.MyTrainings.Api.Core.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Core.Requests.User.SignIn;
using Itis.MyTrainings.Api.Web.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Пользователь"
/// </summary>
public class UserController : BaseController
{
    private readonly IVkService _vkService;
    private readonly IYandexService _yandexService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vkService">Сервис для работы с ВКонтакте</param>
    /// <param name="yandexService">Сервис для работы с Яндекс</param>
    public UserController(
        IVkService vkService,
        IYandexService yandexService)
    {
        _vkService = vkService;
        _yandexService = yandexService;
    }

    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<RegisterUserResponse> RegisterUser(
        [FromServices] IMediator mediator,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new RegisterUserCommand()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email,
                Password = request.Password,
            }, cancellationToken);

    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("signIn")]
    public async Task<SignInResponse> SignIn(
        [FromServices] IMediator mediator,
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new SignInQuery
            {
                Email = request.Email,
                Password = request.Password,
            }, cancellationToken);

    /// <summary>
    /// Отправить код восстановления пароля
    /// </summary>
    /// <param name="mediator">IMediator</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("sendResetPasswordCode")]
    public async Task<SendResetPasswordCodeResponse> SendResetPasswordCode(
        [FromServices] IMediator mediator,
        [FromBody] SendResetPasswordCodeRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new SendResetPasswordQuery
            {
                Email = request.Email
            }, cancellationToken);

    /// <summary>
    /// Сбросить пароль
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost("resetPassword")]
    public async Task<ResetPasswordResponse> ResetPasswordByEmail(
        [FromServices] IMediator mediator,
        [FromBody] ResetPasswordRequest request,
        CancellationToken cancellationToken)
        => await mediator.Send(new ResetPasswordCommand
            {
                Email = request.Email,
                Password = request.Password,
                Code = request.Code,
            }, cancellationToken);

    /// <summary>
    /// Получить информацию о текущем пользователе
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("currentUserInfo")]
    public async Task<GetCurrentUserInfoResponse> GetCurrentUserInfo(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new GetCurrentUserInfoQuery(CurrentUserId), cancellationToken);

    /// <summary>
    /// Авторизировать пользователя через вконтакте
    /// </summary>
    /// <returns>Редирект на форму авторизации</returns>
    [HttpGet("authorizeVk")]
    public async Task<RedirectResult> VkAuthorize() 
        => Redirect(_vkService.GetRedirectToAuthorizationUrl());
    
    /// <summary>
    /// Авторизировать пользователя с помощью Вконтакте
    /// </summary>
    /// <returns></returns>
    [HttpGet("registerWithVk")]
    public async Task<RegisterUserWithVkResponse> RegisterUserWithVk(
        [FromServices] IMediator mediator,
        [FromQuery] string code) =>
        await mediator.Send(new RegisterUserWithVkCommand(code));
    

    /// <summary>
    /// Получить ссылку для авторизации пользователя с помощью Яндекс.
    /// </summary>
    /// <returns></returns>
    [HttpGet("authorizeYandex")]
    public string YandexAuthorize() 
        => _yandexService.GetCodeUri();

    /// <summary>
    /// Регистрация через Яндекс
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="code">Код авторизации</param>
    /// <returns></returns>
    [HttpGet("registeryandex")]
    public async Task<RegisterUserWithYandexResponse> Register(
        [FromServices] IMediator mediator,
        [FromQuery] string code) => 
        await mediator.Send(new RegisterUserWithYandexCommand(code));

    /// <summary>
    /// Проверить пользователя на наличие профиля
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Существует ли профиль пользователя</returns>
    [Policy(PolicyConstants.IsDefaultUser)]
    [HttpGet("checkUserProfile")]
    public async Task<bool> CheckUserProfileAsync(
        [FromServices] IMediator mediator,
        CancellationToken cancellationToken)
        => await mediator.Send(new CheckUserProfileQuery(CurrentUserId), cancellationToken);
}