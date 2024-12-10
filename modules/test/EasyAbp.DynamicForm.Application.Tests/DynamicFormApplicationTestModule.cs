using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(DynamicFormApplicationModule),
    typeof(DynamicFormDomainTestModule)
    )]
public class DynamicFormApplicationTestModule : AbpModule
{

}
