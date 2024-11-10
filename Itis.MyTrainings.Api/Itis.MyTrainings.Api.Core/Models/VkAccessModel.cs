using System.Drawing;
using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Models;

/// <summary>
/// Данные для доступа к VkApi
/// </summary>
public class VkAccessModel
{
    /// <summary>
    /// Uri API вк для авторизации
    /// </summary>
    protected string? VkAuthorizationUri { get; private set; }
    
    /// <summary>
    /// Базовый домен API вк
    /// </summary>
    protected string? VkApiUri { get; private set; }
    
    /// <summary>
    /// Uri редиректа после авторизации
    /// </summary>
    protected string? RedirectUri { get; private set; }
    
    /// <summary>
    /// Id приложения
    /// </summary>
    protected string? AppId { get; private set; }
    
    /// <summary>
    /// Сервисный ключ
    /// </summary>
    protected string? ServiceKey { get; private set; }
    
    /// <summary>
    /// Версия API вк
    /// </summary>
    protected string? Version { get; private set; }

    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public VkAccessModel(IConfiguration configuration)
    {
        AppId = configuration["Vk:AppId"];
        ServiceKey = configuration["Vk:ServiceKey"];
        RedirectUri = configuration["Vk:RedirectUri"];
        Version = configuration["Vk:Version"];
        VkAuthorizationUri = configuration["Vk:VkAuthorizationUri"];
        VkApiUri = configuration["Vk:VkApiUri"];
    }
}