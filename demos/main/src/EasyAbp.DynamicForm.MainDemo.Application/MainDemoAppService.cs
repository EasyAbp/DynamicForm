using EasyAbp.DynamicForm.MainDemo.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.MainDemo;

/* Inherit your application services from this class.
 */
public abstract class MainDemoAppService : ApplicationService
{
    protected MainDemoAppService()
    {
        LocalizationResource = typeof(MainDemoResource);
    }
}
