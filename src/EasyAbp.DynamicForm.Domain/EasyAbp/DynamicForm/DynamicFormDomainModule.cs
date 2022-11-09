using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpDddDomainModule),
    typeof(DynamicFormDomainSharedModule)
)]
public class DynamicFormDomainModule : AbpModule
{
}