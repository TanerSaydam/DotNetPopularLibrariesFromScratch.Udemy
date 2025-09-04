namespace DotnetLibraries.EntityFrameworkCore;

public sealed class DbContextOpitonsBuilder
{
    public DbContextOptions Options { get; set; } = new();
    public void UseSqlServer(string connectionString)
    {
        Options.ConnectionString = connectionString;
    }
}
