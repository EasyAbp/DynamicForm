using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server.Host;

public abstract class UnifiedDemoComponentBase : AbpComponentBase
{
    protected UnifiedDemoComponentBase()
    {
        LocalizationResource = typeof(DynamicFormResource);
    }
}
