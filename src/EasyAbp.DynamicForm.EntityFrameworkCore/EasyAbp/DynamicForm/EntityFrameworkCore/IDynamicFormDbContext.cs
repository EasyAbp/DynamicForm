using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.DynamicForm.Forms;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

[ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
public interface IDynamicFormDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
    DbSet<Form> Forms { get; set; }
}
