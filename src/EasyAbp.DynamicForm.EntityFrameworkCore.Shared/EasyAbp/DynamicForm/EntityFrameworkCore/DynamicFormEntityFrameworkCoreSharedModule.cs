using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

[DependsOn(
    typeof(DynamicFormDomainCoreModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DynamicFormEntityFrameworkCoreSharedModule : AbpModule
{
}