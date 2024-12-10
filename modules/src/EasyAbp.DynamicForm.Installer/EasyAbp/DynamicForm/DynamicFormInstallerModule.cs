using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class DynamicFormInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicFormInstallerModule>();
        });
    }
}
