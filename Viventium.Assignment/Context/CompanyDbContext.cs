using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Viventium.Assignment.Entities;

namespace Viventium.Assignment.Context;

public class CompanyDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public CompanyDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite(_configuration.GetSection("Db")["ConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
    
    public DbSet<CompanyRecord> Companies { get; set; }
    public DbSet<EmployeeRecord> Employees { get; set; }
}