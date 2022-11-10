using Volo.Abp.Json;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpJsonModule),
    typeof(DynamicFormDomainSharedModule)
)]
public class DynamicFormDomainCoreModule : AbpModule
{
}