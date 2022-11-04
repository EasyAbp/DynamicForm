using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm;

[Dependency(ReplaceServices = true)]
public class DynamicFormBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "DynamicForm";
}
