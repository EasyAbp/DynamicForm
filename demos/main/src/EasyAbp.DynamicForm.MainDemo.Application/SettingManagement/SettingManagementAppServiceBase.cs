using EasyAbp.DynamicForm.MainDemo;
using EasyAbp.DynamicForm.MainDemo.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.MainDemo.SettingManagement;

public abstract class SettingManagementAppServiceBase : ApplicationService
{
    protected SettingManagementAppServiceBase()
    {
        ObjectMapperContext = typeof(MainDemoApplicationModule);
        LocalizationResource = typeof(MainDemoResource);
    }
}
