using System.Drawing;
using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Models;

/// <summary>
/// Данные для доступа к VkApi
/// </summary>
public class VkAccessModel
{
    protected string? VkAuthorizationUri { get; private set; }
    protected string? VkApiUri { get; private set; }
    protected string? RedirectUri { get; private set; }
    protected string? AppId { get; private set; }
    protected string? ServiceKey { get; private set; }
    protected string? Version { get; private set; }
    protected string? Scope { get; private set; }
    protected string? ResponseType { get; private set; }
    protected string? AccessToken { get; set; }

    
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public VkAccessModel(IConfiguration configuration)
    {
        AppId = configuration["Vk:AppId"];
        ServiceKey = configuration["Vk:ServiceKey"];
        RedirectUri = configuration["Vk:RedirectUri"];
        Scope = configuration["Vk:Scope"];
        Version = configuration["Vk:Version"];
        VkAuthorizationUri = configuration["Vk:VkAuthorizationUri"];
        VkApiUri = configuration["Vk:VkApiUri"];
    }
}