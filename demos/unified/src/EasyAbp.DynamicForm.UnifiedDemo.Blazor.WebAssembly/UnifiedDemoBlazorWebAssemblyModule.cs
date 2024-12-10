using EasyAbp.DynamicForm.UnifiedDemo.Blazor;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.UnifiedDemo.Blazor.WebAssembly;

[DependsOn(
    typeof(UnifiedDemoBlazorModule)
)]
public class UnifiedDemoBlazorWebAssemblyModule : AbpModule
{
}
