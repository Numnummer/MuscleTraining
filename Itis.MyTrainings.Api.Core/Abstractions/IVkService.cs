using Itis.MyTrainings.Api.Core.Models;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с ВКонтакте
/// </summary>
public interface IVkService
{
    /// <summary>
    /// Получить Url формы авторизации
    /// </summary>
    /// <returns></returns>
    public string GetRedirectToAuthorizationUrl();

    /// <summary>
    /// Получить токен доступа
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    public Task GetAccessTokenAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<VkUserModel> GetVkUserInfoAsync(CancellationToken cancellationToken);

}