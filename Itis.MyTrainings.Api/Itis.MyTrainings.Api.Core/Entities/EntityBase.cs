using Itis.MyTrainings.Api.Core.Abstractions;

namespace Itis.MyTrainings.Api.Core.Entities;

public class EntityBase : IEntity
{
    /// <summary>
    /// ИД сущности
    /// </summary>
    public Guid Id { get; set; }
}