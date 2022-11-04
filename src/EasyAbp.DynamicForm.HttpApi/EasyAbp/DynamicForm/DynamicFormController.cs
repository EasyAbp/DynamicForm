using EasyAbp.DynamicForm.Localization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.DynamicForm;

[Area(DynamicFormRemoteServiceConsts.ModuleName)]
public abstract class DynamicFormController : AbpControllerBase
{
    protected DynamicFormController()
    {
        LocalizationResource = typeof(DynamicFormResource);
    }
}
