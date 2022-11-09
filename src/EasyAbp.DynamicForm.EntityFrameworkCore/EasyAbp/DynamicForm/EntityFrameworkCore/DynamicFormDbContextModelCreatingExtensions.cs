using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Forms;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public static class DynamicFormDbContextModelCreatingExtensions
{
    public static void ConfigureDynamicForm(
        this ModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));

        builder.Entity<Form>(b =>
        {
            b.ToTable(DynamicFormDbProperties.DbTablePrefix + "Forms", DynamicFormDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.HasIndex(x => x.FormTemplateId);
        });

        builder.Entity<FormItem>(b =>
        {
            b.ToTable(DynamicFormDbProperties.DbTablePrefix + "FormItems", DynamicFormDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.TryConfigureAvailableValues();

            /* Configure more properties here */
            b.HasKey(x => new { x.FormId, x.Name });
        });

        builder.Entity<FormTemplate>(b =>
        {
            b.ToTable(DynamicFormDbProperties.DbTablePrefix + "FormTemplates", DynamicFormDbProperties.DbSchema);
            b.ConfigureByConvention();

            /* Configure more properties here */
            b.HasIndex(x => x.FormDefinitionName);
            b.HasIndex(x => x.CustomTag);
        });

        builder.Entity<FormItemTemplate>(b =>
        {
            b.ToTable(DynamicFormDbProperties.DbTablePrefix + "FormItemTemplates", DynamicFormDbProperties.DbSchema);
            b.ConfigureByConvention();
            b.TryConfigureAvailableValues();

            /* Configure more properties here */
            b.HasKey(x => new { x.FormTemplateId, x.Name });
        });
    }
}