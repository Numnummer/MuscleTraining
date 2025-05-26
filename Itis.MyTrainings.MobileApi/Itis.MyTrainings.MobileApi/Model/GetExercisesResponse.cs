namespace Itis.MyTrainings.MobileApi.Model;

public class GetExercisesResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="totalCount">Общее кол-во</param>
    /// <param name="items">Сущности</param>
    public GetExercisesResponse(int totalCount, List<Exercise> items)
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
    public List<Exercise> Items { get; set; }
}