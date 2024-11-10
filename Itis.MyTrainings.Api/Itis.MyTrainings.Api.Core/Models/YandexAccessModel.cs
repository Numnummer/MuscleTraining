using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Models;

/// <summary>
/// Данные для доступа к яндексу
/// </summary>
public class YandexAccessModel
{
    protected string? GrantType { get; set; }
    protected string? ClientId { get; set; }
    protected string? ClientSecret { get; set; }
    protected string? RedirectUri { get; set; }
    protected string? BaseUri { get; set; }
    protected string? LoginUri { get; set; }
    protected string? ResponseType { get; set; }

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration">Конфигурация</param>
    public YandexAccessModel(IConfiguration configuration)
    {
        GrantType = configuration["Yandex:GrantType"];
        ClientId = configuration["Yandex:ClientId"];
        ClientSecret = configuration["Yandex:ClientSecret"];
        RedirectUri = configuration["Yandex:RedirectUri"];
        BaseUri = configuration["Yandex:BaseUri"];
        LoginUri = configuration["Yandex:LoginUri"];
        ResponseType = configuration["Yandex:ResponseType"];
    }
}