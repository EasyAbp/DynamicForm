using EasyAbp.DynamicForm.UnifiedDemo.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.UnifiedDemo;

/* Inherit your application services from this class.
 */
public abstract class UnifiedDemoAppService : ApplicationService
{
    protected UnifiedDemoAppService()
    {
        LocalizationResource = typeof(UnifiedDemoResource);
    }
}
