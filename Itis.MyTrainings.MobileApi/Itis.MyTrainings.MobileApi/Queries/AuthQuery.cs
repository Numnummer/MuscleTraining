using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Queries;

public class AuthQuery(IHttpClientFactory httpClientFactory)
{
    public async Task<AuthResponse> Login(string email, string password)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var signInRequest = new SignInRequest()
        {
            Email = email,
            Password = password
        };
        var response = await client.PostAsJsonAsync("api/user/signIn", signInRequest);
        response.EnsureSuccessStatusCode();
        var signInResponse = await response.Content.ReadFromJsonAsync<SignInResponse>();
        var a= await response.Content.ReadAsStringAsync();
        
        return new AuthResponse()
        {
            Message = "Done",
            Success = true,
            Token = signInResponse.JwtToken
        };
    }
}