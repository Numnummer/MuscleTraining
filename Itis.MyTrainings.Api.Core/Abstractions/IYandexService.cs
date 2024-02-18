using Itis.MyTrainings.Api.Core.Models;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с Яндекс.
/// </summary>
public interface IYandexService
{
    /// <summary>
    /// Получить Url формы авторизации
    /// </summary>
    /// <returns></returns>
    public  string GetCodeUri();

    /// <summary>
    /// Получение access-token .
    /// </summary>
    /// <param name="code">Токен, для обмена на access_token</param>
    /// <param name="cancellationToken">Токен  отмены запроса</param>
    /// <returns></returns>
    public Task<AccessTokenResponse> GetAccessTokenAsync(string code, CancellationToken cancellationToken);

    /// <summary>
    /// Получение информации о пользователе .
    /// </summary>
    /// <param name="accessToken">Токен для доступа к данным</param>
    /// <param name="cancellationToken">Токен  отмены запроса</param>
    /// <returns></returns>
    public Task<YandexUserModel> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken);

}