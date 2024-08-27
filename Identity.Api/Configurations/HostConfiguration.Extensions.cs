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
        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString);});
        
        return builder;
    }

    /// <summary>
    /// Registers developer tools
    /// </summary>
    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen();
        
        return builder;
    }

    /// <summary>
    /// Registers API exposers
    /// </summary>
    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.AddRouting(options => { options.LowercaseUrls = true; });
        builder.Services.AddControllers().AddNewtonsoftJson();

        return builder;
    }
    
    /// <summary>
    /// Registers developer tools middlewares
    /// </summary>
    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        
        return app;
    }

    /// <summary>
    /// Registers exposer middlewares
    /// </summary>
    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();
        
        return app;
    }
    
}