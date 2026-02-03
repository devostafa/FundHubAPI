using FundHub.Data.Data.Models;
using FundHub.Data.Services.PasswordHash;
using FundHub.Data.Services.Seed;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace FundHub.Data.Data;

public class DataContext : DbContext
{
    private readonly IConfiguration _configEnv;
    private readonly IPasswordHash _passwordHash;

    public DataContext(IConfiguration configEnv, IPasswordHash passwordHash)
    {
        _configEnv = configEnv;
        _passwordHash = passwordHash;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configEnv.GetConnectionString("DatabaseConnection"));
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Seed.SeedDatabase(modelBuilder, _passwordHash); // Runs during bootup, better than at runtime
    }

    public DbSet<Project> Projects { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<News> News { get; set; }
    public DbSet<Donation> DonationLogs { get; set; }
    public DbSet<Category> Categories { get; set; }
}