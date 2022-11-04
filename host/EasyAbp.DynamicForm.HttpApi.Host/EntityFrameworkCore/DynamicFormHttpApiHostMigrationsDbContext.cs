using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public class DynamicFormHttpApiHostMigrationsDbContext : AbpDbContext<DynamicFormHttpApiHostMigrationsDbContext>
{
    public DynamicFormHttpApiHostMigrationsDbContext(DbContextOptions<DynamicFormHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureDynamicForm();
    }
}
