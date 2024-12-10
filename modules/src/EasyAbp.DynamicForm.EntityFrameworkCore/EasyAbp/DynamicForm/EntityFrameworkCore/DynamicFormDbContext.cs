using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.FormTemplates;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

[ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
public class DynamicFormDbContext : AbpDbContext<DynamicFormDbContext>, IDynamicFormDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */
    public DbSet<Form> Forms { get; set; }
    public DbSet<FormItem> FormItems { get; set; }
    public DbSet<FormTemplate> FormTemplates { get; set; }
    public DbSet<FormItemTemplate> FormItemTemplates { get; set; }

    public DynamicFormDbContext(DbContextOptions<DynamicFormDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureDynamicForm();
    }
}