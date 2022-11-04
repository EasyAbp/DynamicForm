using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(DynamicFormApplicationContractsModule),
    typeof(AbpHttpClientModule))]
public class DynamicFormHttpApiClientModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddHttpClientProxies(
            typeof(DynamicFormApplicationContractsModule).Assembly,
            DynamicFormRemoteServiceConsts.RemoteServiceName
        );

        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicFormHttpApiClientModule>();
        });

    }
}
