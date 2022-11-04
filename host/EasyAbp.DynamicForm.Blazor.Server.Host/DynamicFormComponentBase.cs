using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Components;

namespace EasyAbp.DynamicForm.Blazor.Server.Host;

public abstract class DynamicFormComponentBase : AbpComponentBase
{
    protected DynamicFormComponentBase()
    {
        LocalizationResource = typeof(DynamicFormResource);
    }
}
