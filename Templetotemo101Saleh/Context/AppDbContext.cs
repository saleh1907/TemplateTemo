using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Templetotemo101Saleh.Models;

namespace Templetotemo101Saleh.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }

   public DbSet<Product>Products { get; set; }
    public DbSet<Category>Categories { get; set; }  
}
