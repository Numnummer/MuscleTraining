using Itis.MyTrainings.Api.Core.Abstractions;

namespace Itis.MyTrainings.Api.Core.Services;

/// <summary>
/// Сервис работы с http запрсоами
/// </summary>
public class HttpHelperService: IHttpHelperService
{
    private HttpClient _httpClient;

    /// <summary>
    /// Конструктор
    /// </summary>
    public HttpHelperService()
    {
        _httpClient = new HttpClient();
    }
    
    /// <inheritdoc />
    public async Task<HttpResponseMessage> GetAsync(string uri, CancellationToken cancellationToken)
        => await _httpClient.GetAsync(uri);

    /// <inheritdoc />
    public async Task<HttpResponseMessage> PostAsync(string uri, HttpContent content, CancellationToken cancellationToken)
        => await _httpClient.PostAsync(uri, content);

    /// <inheritdoc />
    public void SetAuthorizationHeader(string acces_token, CancellationToken cancellationToken)
    {
        _httpClient.DefaultRequestHeaders.Add("Authorization","Bearer "+acces_token);
    }
}