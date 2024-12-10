using System;
using Volo.Abp.Data;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.MongoDB;

[DependsOn(
    typeof(DynamicFormTestBaseModule),
    typeof(DynamicFormMongoDbModule)
    )]
public class DynamicFormMongoDbTestModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpDbConnectionOptions>(options =>
        {
            options.ConnectionStrings.Default = MongoDbFixture.GetRandomConnectionString();
        });
    }
}
