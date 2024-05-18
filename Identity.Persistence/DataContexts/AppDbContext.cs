using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Persistence.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Client> Clients => Set<Client>();
    
    protected override void OnModelCreating( ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}