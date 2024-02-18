using System.Collections.Specialized;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Constants;
using Itis.MyTrainings.Api.Core.Models;
using Microsoft.Extensions.Configuration;

namespace Itis.MyTrainings.Api.Core.Services;

/// <summary>
/// Сервис для работы с ВКонтакте
/// </summary>
public class VkService : VkAccessModel, IVkService
{
    private IHttpHelperService _httpHelperService;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="httpHelperService">Сервис работы с http запрсоами</param>
    /// <param name="configuration">Конфигурация</param>
    public VkService(
        IHttpHelperService httpHelperService, 
        IConfiguration configuration)
    : base(configuration)
    {
        _httpHelperService = httpHelperService;
    }

    /// <inheritdoc />
    public async Task<VkUserModel> GetVkUserInfoAsync(CancellationToken cancellationToken)
    {
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(AccessToken!), "access_token");
        content.Add(new StringContent(Version!), "v");

        var response = await _httpHelperService.PostAsync(
            $"{VkApiUri}method/account.getProfileInfo", content, cancellationToken);

        return await JsonSerializer.DeserializeAsync<VkUserModel>(await response.Content.ReadAsStreamAsync());
    }

    /// <inheritdoc />
    public async Task GetAccessTokenAsync(string code, CancellationToken cancellationToken)
    {
        //TODO в try catch
        var response = await _httpHelperService.GetAsync(
            $"{VkAuthorizationUri}access_token?client_id={AppId}&client_secret={ServiceKey}&redirect_uri={RedirectUri}&code={code}",
            cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Не получилось достучаться до Вк");
        
        //TODO в try catch
       var model = await JsonSerializer.DeserializeAsync<AccessTokenResponse>(
           await response.Content.ReadAsStreamAsync());

       if (model?.Error != null)
           throw new HttpRequestException(model.ErrorDescription);

       AccessToken = model.AccessToken;
    }
    
    /// <inheritdoc /> // TODO: вынести в appsettings.json
    public string GetRedirectToAuthorizationUrl() => 
        $"{VkAuthorizationUri}authorize?client_id={AppId}&scope={Scope}&response_type={ResponseType}" +
        $"&v={Version}&redirect_uri={RedirectUri}";
}