using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DynamicFormDomainCoreModule)
)]
public class DynamicFormDomainModule : AbpModule
{
}