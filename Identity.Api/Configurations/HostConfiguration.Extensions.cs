using System.Reflection;
using Identity.Domain.Constants;
using Identity.Persistence.DataContexts;
using Microsoft.EntityFrameworkCore;

namespace Identity.Api.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies = Assembly
        .GetExecutingAssembly()
        .GetReferencedAssemblies()
        .Select(Assembly.Load)
        .Append(Assembly.GetExecutingAssembly())
        .ToList();

    
    /// <summary>
    /// Registers persistence infrastructure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        // Determine connection string
        var dbConnectionString = builder.Environment.IsProduction()
            ? Environment.GetEnvironmentVariable(DataAccessConstant.DbConnectionString)
            : builder.Configuration.GetConnectionString(DataAccessConstant.DbConnectionString);
        
        // Register data context
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString); });
        
        return builder;
    }
    
}