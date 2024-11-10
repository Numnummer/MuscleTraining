namespace Itis.MyTrainings.Api.Contracts.Requests.Training.GetTrainings;

/// <summary>
/// Ответ на <see cref="GetTrainingsRequest"/>
/// </summary>
public class GetTrainingsResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="totalCount">Кол-во сущностей</param>
    /// <param name="items">Сущности</param>
    public GetTrainingsResponse(
        int totalCount,
        List<GetTrainingsResponseItem> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
    
    /// <summary>
    /// Кол-во сущностей
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Сущности
    /// </summary>
    public List<GetTrainingsResponseItem> Items { get; set; }
}