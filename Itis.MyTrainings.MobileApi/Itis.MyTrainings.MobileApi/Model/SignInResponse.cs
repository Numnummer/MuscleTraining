using System.Text.Json.Serialization;

namespace Itis.MyTrainings.MobileApi.Model;

public class SignInResponse
{
    [JsonPropertyName("jwtToken")]
    public string JwtToken { get; set; }
}