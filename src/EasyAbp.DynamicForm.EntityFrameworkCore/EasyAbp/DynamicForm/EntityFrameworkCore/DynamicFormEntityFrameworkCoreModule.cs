using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Forms;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

[DependsOn(
    typeof(DynamicFormDomainModule),
    typeof(DynamicFormEntityFrameworkCoreSharedModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class DynamicFormEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<DynamicFormDbContext>(options =>
        {
                /* Add custom repositories here. Example:
                 * options.AddRepository<Question, EfCoreQuestionRepository>();
                 */
            options.AddRepository<Form, FormRepository>();
            options.AddRepository<FormTemplate, FormTemplateRepository>();
        });
    }
}
