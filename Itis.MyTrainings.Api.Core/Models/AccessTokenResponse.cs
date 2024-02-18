using System.Text.Json.Serialization;

namespace Itis.MyTrainings.Api.Core.Models;

public class AccessTokenResponse
{
    // public AccessTokenResponse(
    //     string? accessToken,
    //     int? expiresIn
    //     )
    // {
    //     
    // }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; set; }
    
    [JsonPropertyName("error")]
    public string? Error { get; set; }
    
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; }
}