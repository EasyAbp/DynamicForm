using EasyAbp.DynamicForm.Options;
using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpJsonModule),
    typeof(DynamicFormDomainSharedModule)
)]
public class DynamicFormDomainCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<DynamicFormCoreOptions>(options => { options.AddBuiltInFormItemTypes(); });
    }
}