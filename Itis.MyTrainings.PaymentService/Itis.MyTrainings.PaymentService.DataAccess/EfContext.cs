using Itis.MyTrainings.PaymentService.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.PaymentService.DataAccess;

public class EfContext:DbContext
{
    public EfContext(DbContextOptions<EfContext> options):base(options)
    {
        
    }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Transaction>()
            .HasKey(x => x.Id);
        modelBuilder.Entity<Transaction>()
            .HasIndex(x=>new {x.Iteration,x.ProductCountRemains}).IsUnique();

    }
}