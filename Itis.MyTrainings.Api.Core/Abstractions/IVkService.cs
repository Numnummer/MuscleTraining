using Itis.MyTrainings.Api.Core.Models;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с ВКонтакте
/// </summary>
public interface IVkService
{
    /// <summary>
    /// Получить информацию о пользователе
    /// </summary>
    /// <param name="code">Код для авторизации</param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<VkUserModel> GetVkUserInfoAsync(string code, CancellationToken cancellationToken);

}