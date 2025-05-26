using System.Net.Http.Headers;
using Itis.MyTrainings.MobileApi.Model;

namespace Itis.MyTrainings.MobileApi.Queries;

public class ExerciseQuery(IHttpClientFactory httpClientFactory)
{
    public async Task<List<Exercise>> GetAllExercises(string jwtToken)
    {
        var client = httpClientFactory.CreateClient("ApiClient");
        client.DefaultRequestHeaders.Authorization = 
            new AuthenticationHeaderValue("Bearer", jwtToken);
        var response = await client.GetAsync("api/exercises");
        response.EnsureSuccessStatusCode();
        var exercises = await response.Content.ReadFromJsonAsync<GetExercisesResponse>();
        if (exercises.Items == null)
        {
            return new List<Exercise>();
        }
        return exercises.Items;
    }

    public async Task<Exercise> GetExercise(string name)
    {
        throw new NotImplementedException();
    }
}