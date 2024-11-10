namespace Itis.MyTrainings.Api.Contracts.Requests.Exercise.GetExercises;

/// <summary>
/// Ответ на <see cref="GetExercisesRequest"/>
/// </summary>
public class GetExercisesResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="totalCount">Общее кол-во</param>
    /// <param name="items">Сущности</param>
    public GetExercisesResponse(int totalCount, List<GetExercisesResponseItem> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
    
    /// <summary>
    /// Общее кол-во
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Сущности
    /// </summary>
    public List<GetExercisesResponseItem> Items { get; set; }
}