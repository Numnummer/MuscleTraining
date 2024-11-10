using System.Text.Json;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Models;
using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Services;

/// <summary>
/// Сервис для авторизации через Яндекс.
/// </summary>
public class YandexService : YandexAccessModel, IYandexService
{
    private readonly IHttpHelperService _httpHelperService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="configuration"> Сикреты приложения</param>
    /// <param name="httpHelperService">Сервис для GET/POST запросов</param>
    public YandexService(
        IConfiguration configuration,
        IHttpHelperService httpHelperService)
    : base(configuration)
    {
        _httpHelperService = httpHelperService;
    }

    ///<inheritdoc />
    public string GetCodeUri() => 
        $"{BaseUri}authorize?response_type={ResponseType}&client_id={ClientId}";

    ///<inheritdoc />
    public async Task<AccessTokenResponse> GetAccessTokenAsync(string code, CancellationToken cancellationToken)
    {
        try
        {
            var response = await _httpHelperService.PostAsync( $"{BaseUri}token",
                new FormUrlEncodedContent(new List<KeyValuePair<string, string>>()
                {
                    new("grant_type", GrantType!),
                    new("code", code),
                    new("client_id", ClientId!),
                    new("client_secret", ClientSecret!),
                }),
                cancellationToken);
            
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException("Яндекс дал заднюю и не стал делиться токеном");
            
            return (await JsonSerializer.DeserializeAsync<AccessTokenResponse>(
                await response.Content.ReadAsStreamAsync(), 
                cancellationToken: cancellationToken))!;
        }
        catch (Exception)
        {
            throw new HttpRequestException("Что-то пошло не так");
        }
    }

    ///<inheritdoc />
    public async Task<YandexUserModel> GetUserInfoAsync(string accessToken, CancellationToken cancellationToken)
    {
        try
        {
            _httpHelperService.SetAuthorizationHeader(accessToken, cancellationToken: cancellationToken);
            var response = await  _httpHelperService.GetAsync(LoginUri!, cancellationToken);

            if (!response.IsSuccessStatusCode)
                throw new Exception();
            
            return  (await JsonSerializer.DeserializeAsync<YandexUserModel>(
                await response.Content.ReadAsStreamAsync(),
                cancellationToken: cancellationToken))!;
        }
        catch (Exception e)
        {
            throw new HttpRequestException("Не получилось взять информацию");
        }
    }
}
