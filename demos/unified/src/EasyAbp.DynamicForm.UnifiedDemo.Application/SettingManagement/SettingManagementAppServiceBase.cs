using EasyAbp.DynamicForm.UnifiedDemo;
using EasyAbp.DynamicForm.UnifiedDemo.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.UnifiedDemo.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(UnifiedDemoApplicationModule);
        LocalizationResource = typeof(UnifiedDemoResource);
    }
}
