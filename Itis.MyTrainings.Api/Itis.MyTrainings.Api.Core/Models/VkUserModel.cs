using System.Text.Json.Serialization;

namespace Itis.MyTrainings.Api.Core.Models;

// Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
public class Country
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }
}

public class Response
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("home_town")]
    public string HomeTown { get; set; }

    [JsonPropertyName("status")]
    public string Status { get; set; }

    [JsonPropertyName("photo_200")]
    public string Photo200 { get; set; }

    [JsonPropertyName("is_service_account")]
    public bool IsServiceAccount { get; set; }

    [JsonPropertyName("bdate")]
    public string Bdate { get; set; }

    [JsonPropertyName("is_verified")]
    public bool IsVerified { get; set; }

    [JsonPropertyName("mail")]
    public string Mail { get; set; }

    [JsonPropertyName("verification_status")]
    public string VerificationStatus { get; set; }

    [JsonPropertyName("promo_verifications")]
    public List<object> PromoVerifications { get; set; }

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string LastName { get; set; }

    [JsonPropertyName("bdate_visibility")]
    public int BdateVisibility { get; set; }

    [JsonPropertyName("country")]
    public Country Country { get; set; }

    [JsonPropertyName("phone")]
    public string Phone { get; set; }

    [JsonPropertyName("relation")]
    public int Relation { get; set; }

    [JsonPropertyName("sex")]
    public int Sex { get; set; }
}

public class VkUserModel
{
    [JsonPropertyName("response")]
    public Response Response { get; set; }
}


