using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public class DynamicFormHttpApiHostMigrationsDbContextFactory : IDesignTimeDbContextFactory<DynamicFormHttpApiHostMigrationsDbContext>
{
    public DynamicFormHttpApiHostMigrationsDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<DynamicFormHttpApiHostMigrationsDbContext>()
            .UseSqlServer(configuration.GetConnectionString("DynamicForm"));

        return new DynamicFormHttpApiHostMigrationsDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
