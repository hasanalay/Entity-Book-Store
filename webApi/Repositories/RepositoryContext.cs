using Microsoft.EntityFrameworkCore;
using webApi.Models;
using webApi.Repositories.Config;

namespace webApi.Repositories;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options) 
    {
    }
    public DbSet<Book>Books { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfig());
    }
}