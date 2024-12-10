using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.Threading;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using EasyAbp.DynamicForm.Blazor;
using Volo.Abp.AspNetCore.Components.Web.Theming;
using EasyAbp.DynamicForm.MainDemo;

namespace EasyAbp.DynamicForm.MainDemo.Blazor;

[DependsOn(typeof(AbpAutoMapperModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsWebThemingModule))]
//app modules
[DependsOn(typeof(MainDemoApplicationContractsModule))]
[DependsOn(typeof(DynamicFormBlazorModule))]

public class MainDemoBlazorModule : AbpModule
{
    private static readonly OneTimeRunner OneTimeRunner = new OneTimeRunner();

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MainDemoBlazorModule>();

        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<MainDemoBlazorAutoMapperProfile>(validate: true);
        });

        //Configure<AbpNavigationOptions>(options =>
        //{
        //    options.MenuContributors.Add(new DemoMenuContributor());
        //});

        Configure<AbpRouterOptions>(options =>
        {
            options.AdditionalAssemblies.Add(typeof(MainDemoBlazorModule).Assembly);
        });
    }
}
