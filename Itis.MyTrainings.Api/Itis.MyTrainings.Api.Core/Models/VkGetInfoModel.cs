using System.Text.Json.Serialization;

namespace Itis.MyTrainings.Api.Core.Models;

[Serializable]
public class VkGetInfoModel
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("v")]
    public string Version { get; set; }
}