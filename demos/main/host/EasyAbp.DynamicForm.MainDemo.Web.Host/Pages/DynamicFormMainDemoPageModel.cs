using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.DynamicForm.MainDemo.Pages;

public abstract class DynamicFormMainDemoPageModel : AbpPageModel
{
    protected DynamicFormMainDemoPageModel()
    {
        LocalizationResourceType = typeof(DynamicFormResource);
    }
}
