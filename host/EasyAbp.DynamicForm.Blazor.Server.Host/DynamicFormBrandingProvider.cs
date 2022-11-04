using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace EasyAbp.DynamicForm.Blazor.Server.Host;

[Dependency(ReplaceServices = true)]
public class DynamicFormBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicForm";
}
