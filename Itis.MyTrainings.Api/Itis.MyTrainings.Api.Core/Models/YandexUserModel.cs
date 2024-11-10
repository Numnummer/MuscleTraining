using System.Text.Json.Serialization;

namespace Itis.MyTrainings.Api.Core.Models;

public class YandexUserModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("login")]
    public string Login { get; set; }

    [JsonPropertyName("birthday")]
    public string Bdate { get; set; }

    [JsonPropertyName("emails")]
    public List<string> Mails { get; set; }

    [JsonPropertyName("default_email")]
    public string DefaultEmail { get; set; }
    
    [JsonPropertyName("default_phone")]
    public DefaultPhone Phone { get; set; }
    
    [JsonPropertyName("display_name")]
    public string DisplayName { get; set; }
    
    [JsonPropertyName("real_name")]
    public string RealName { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("sex")]
    public string Sex { get; set; }

}

public class DefaultPhone
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("number")]
    public string Number { get; set; }
}