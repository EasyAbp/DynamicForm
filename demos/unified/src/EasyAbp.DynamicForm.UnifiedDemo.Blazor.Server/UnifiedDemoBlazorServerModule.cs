using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server;

[DependsOn(
    typeof(UnifiedDemoBlazorModule)
)]
public class UnifiedDemoBlazorServerModule : AbpModule
{

}
