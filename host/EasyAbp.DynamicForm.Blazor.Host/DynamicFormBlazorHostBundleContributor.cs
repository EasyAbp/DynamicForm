using Volo.Abp.Bundling;

namespace EasyAbp.DynamicForm.Blazor.Host;

public class DynamicFormBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
