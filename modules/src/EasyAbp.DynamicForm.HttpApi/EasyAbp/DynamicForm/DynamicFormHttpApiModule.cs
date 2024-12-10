using Localization.Resources.AbpUi;
using EasyAbp.DynamicForm.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(DynamicFormApplicationContractsModule),
    typeof(AbpAspNetCoreMvcModule))]
public class DynamicFormHttpApiModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        PreConfigure<IMvcBuilder>(mvcBuilder =>
        {
            mvcBuilder.AddApplicationPartIfNotExists(typeof(DynamicFormHttpApiModule).Assembly);
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Get<DynamicFormResource>()
                .AddBaseTypes(typeof(AbpUiResource));
        });
    }
}
