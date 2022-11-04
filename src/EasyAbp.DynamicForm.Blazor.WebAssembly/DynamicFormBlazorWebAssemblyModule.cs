using Volo.Abp.AspNetCore.Components.WebAssembly.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.Blazor.WebAssembly;

[DependsOn(
    typeof(DynamicFormBlazorModule),
    typeof(DynamicFormHttpApiClientModule),
    typeof(AbpAspNetCoreComponentsWebAssemblyThemingModule)
    )]
public class DynamicFormBlazorWebAssemblyModule : AbpModule
{

}
