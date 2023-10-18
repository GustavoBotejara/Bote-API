using Microsoft.EntityFrameworkCore;

namespace BoteApi.Db;

public record Bote 
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class BoteDb : DbContext
{

    public BoteDb(DbContextOptions options) : base(options) { }
    public DbSet<Bote> Botes { get; set; } = null!;
}