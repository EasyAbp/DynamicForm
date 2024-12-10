using EasyAbp.DynamicForm.UnifiedDemo.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.UnifiedDemo.SqlServer.EntityFrameworkCore;

[DependsOn(typeof(UnifiedDemoEntityFrameworkCoreModule))]
public class UnifiedDemoEntityFrameworkCoreSqlServerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<UnifiedDemoMigrationsDbContext>();
    }
}