// Plik: DataContext.cs
using Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Api.Entities;

namespace Api.Helpers;


public class DataContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DataContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to sql server database
        options.UseSqlServer(Configuration.GetConnectionString("ApiDatabase"));
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
}
