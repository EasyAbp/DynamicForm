using Volo.Abp.AspNetCore.Components.Server.Theming;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.Blazor.Server;

[DependsOn(
    typeof(AbpAspNetCoreComponentsServerThemingModule),
    typeof(DynamicFormBlazorModule)
    )]
public class DynamicFormBlazorServerModule : AbpModule
{

}
