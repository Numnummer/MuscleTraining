using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Itis.MyTrainings.Api.PostgreSql.Configuration;

internal class ProductConfiguration:EntityBaseConfiguration<Product>
{
    public override void ConfigureChild(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.HasData([
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "asd",
                Price = 10,
                Remains = 20,
                Description = "asd"
            }
        ]);
    }
}