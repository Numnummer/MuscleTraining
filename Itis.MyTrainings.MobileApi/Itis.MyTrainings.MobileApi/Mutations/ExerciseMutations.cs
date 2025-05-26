using System.Net.Http.Headers;
using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Mutations;

public class ExerciseMutations(IHttpClientFactory httpClientFactory):ObjectType
{
    public async Task AddExercise(string jwtToken, string name, string description)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", jwtToken);
        var request = new PostExerciseRequest()
        {
            Name = name,
            Description = description,
        };
        var response = await client.PostAsJsonAsync("api/exercises", request);
        response.EnsureSuccessStatusCode();
    }
}