using Volo.Abp.Bundling;

namespace EasyAbp.DynamicForm.MainDemo.Blazor.Host;

public class DynamicFormMainDemoBlazorHostBundleContributor : IBundleContributor
{
    public void AddScripts(BundleContext context)
    {

    }

    public void AddStyles(BundleContext context)
    {
        context.Add("main.css", true);
    }
}
