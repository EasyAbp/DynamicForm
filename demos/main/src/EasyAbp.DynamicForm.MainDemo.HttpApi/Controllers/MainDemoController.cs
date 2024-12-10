using EasyAbp.DynamicForm.MainDemo.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace EasyAbp.DynamicForm.MainDemo.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class MainDemoController : AbpControllerBase
{
    protected MainDemoController()
    {
        LocalizationResource = typeof(MainDemoResource);
    }
}
