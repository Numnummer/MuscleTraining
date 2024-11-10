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
    private readonly IHttpHelperService _httpHelperService;

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
    public async Task<VkUserModel> GetVkUserInfoAsync(string code, CancellationToken cancellationToken)
    {
        var model = await GetAccessTokenAsync(code, cancellationToken);
        
        var content = new MultipartFormDataContent();
        content.Add(new StringContent(model.AccessToken!), "access_token");
        content.Add(new StringContent(Version!), "v");

        var response = await _httpHelperService.PostAsync(
            $"{VkApiUri}method/account.getProfileInfo", content, cancellationToken);

        var result =  await JsonSerializer.DeserializeAsync<VkUserModel>(
            await response.Content.ReadAsStreamAsync(cancellationToken),
            cancellationToken: cancellationToken);
        result!.Response.Mail = model!.Email!;

        return result;
    }

    private async Task<AccessTokenResponse> GetAccessTokenAsync(string code, CancellationToken cancellationToken)
    {
        var response = await _httpHelperService.GetAsync(
            $"{VkAuthorizationUri}access_token?client_id={AppId}&client_secret={ServiceKey}&redirect_uri={RedirectUri}&code={code}",
            cancellationToken);

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException("Не получилось достучаться до Вк");
        
        var model = await JsonSerializer.DeserializeAsync<AccessTokenResponse>(
            await response.Content.ReadAsStreamAsync(cancellationToken), 
            cancellationToken: cancellationToken);

        if (model?.Error != null)
            throw new HttpRequestException(model.ErrorDescription);
       
        return model!;
    }
}