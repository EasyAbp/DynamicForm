using EasyAbp.DynamicForm.Localization;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm;

public abstract class DynamicFormAppService : ApplicationService
{
    protected DynamicFormAppService()
    {
        LocalizationResource = typeof(DynamicFormResource);
        ObjectMapperContext = typeof(DynamicFormApplicationModule);
    }
}
