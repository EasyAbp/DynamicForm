using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(DynamicFormDomainSharedModule),
    typeof(AbpDddApplicationContractsModule),
    typeof(AbpAuthorizationModule)
    )]
public class DynamicFormApplicationContractsModule : AbpModule
{

}
