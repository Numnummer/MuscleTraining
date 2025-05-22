using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Mutations;

public class AuthMutations(IHttpClientFactory httpClientFactory)
{
    public async Task<AuthResponse> Register(string username, string email, string password)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        var request = new RegisterUserRequest()
        {
            Email = email,
            UserName = username,
            FirstName = username,
            LastName = username,
            Role = "User",
            Password = password
        };
        var response = await client.PostAsJsonAsync("api/user/register", request);
        response.EnsureSuccessStatusCode();
        var signInRequest = new SignInRequest()
        {
            Email = email,
            Password = password
        };
        response = await client.PostAsJsonAsync("api/user/signIn", signInRequest);
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