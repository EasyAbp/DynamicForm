using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace EasyAbp.DynamicForm.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class DynamicFormPageModel : AbpPageModel
{
    protected DynamicFormPageModel()
    {
        LocalizationResourceType = typeof(DynamicFormResource);
        ObjectMapperContext = typeof(DynamicFormWebModule);
    }
}
