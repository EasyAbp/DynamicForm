using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.MainDemo.Blazor.Server;

[DependsOn(
    typeof(MainDemoBlazorModule)
)]
public class MainDemoBlazorServerModule : AbpModule
{

}
