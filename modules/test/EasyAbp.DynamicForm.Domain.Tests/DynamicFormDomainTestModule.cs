using EasyAbp.DynamicForm.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

/* Domain tests are configured to use the EF Core provider.
 * You can switch to MongoDB, however your domain tests should be
 * database independent anyway.
 */
[DependsOn(
    typeof(DynamicFormEntityFrameworkCoreTestModule)
    )]
public class DynamicFormDomainTestModule : AbpModule
{

}
