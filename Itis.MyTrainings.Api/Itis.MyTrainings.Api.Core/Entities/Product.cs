namespace Itis.MyTrainings.Api.Core.Entities;

public class Product : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public uint Price { get; set; }
    public uint Remains { get; set; }
}