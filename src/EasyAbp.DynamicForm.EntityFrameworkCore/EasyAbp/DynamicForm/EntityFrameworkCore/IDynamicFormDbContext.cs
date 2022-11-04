using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

[ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
public interface IDynamicFormDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
