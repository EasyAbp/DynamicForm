using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.DynamicForm.Pages;

public abstract class DynamicFormPageModel : AbpPageModel
{
    protected DynamicFormPageModel()
    {
        LocalizationResourceType = typeof(DynamicFormResource);
    }
}
