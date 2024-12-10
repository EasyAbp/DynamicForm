using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class UnifiedDemoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicForm";
}
