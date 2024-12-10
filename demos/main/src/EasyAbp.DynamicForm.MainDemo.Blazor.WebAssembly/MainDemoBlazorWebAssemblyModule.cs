using EasyAbp.DynamicForm.MainDemo.Blazor;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.MainDemo.Blazor.WebAssembly;

[DependsOn(
    typeof(MainDemoBlazorModule)
)]
public class MainDemoBlazorWebAssemblyModule : AbpModule
{
}
