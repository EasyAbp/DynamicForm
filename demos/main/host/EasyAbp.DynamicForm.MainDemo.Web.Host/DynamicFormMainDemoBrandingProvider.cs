using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.MainDemo;

[Dependency(ReplaceServices = true)]
public class DynamicFormMainDemoBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicForm";
}
