using EasyAbp.DynamicForm.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.MainDemo.EntityFrameworkCore;

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
