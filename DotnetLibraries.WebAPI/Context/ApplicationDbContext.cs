using DotnetLibraries.EntityFrameworkCore;
using DotnetLibraries.WebAPI.Models;

namespace DotnetLibraries.WebAPI.Context;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
}
