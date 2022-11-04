using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using EasyAbp.DynamicForm.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpValidationModule)
)]
public class DynamicFormDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<DynamicFormDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<DynamicFormResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/EasyAbp/DynamicForm/Localization");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("EasyAbp.DynamicForm", typeof(DynamicFormResource));
        });
    }
}
