using EasyAbp.DynamicForm;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.VirtualFileSystem;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.PermissionManagement;

namespace EasyAbp.DynamicForm.MainDemo;

[DependsOn(typeof(DemoDomainModule))]
[DependsOn(typeof(MainDemoApplicationContractsModule))]

[DependsOn(typeof(AbpAccountApplicationModule))]
[DependsOn(typeof(AbpIdentityApplicationModule))]
[DependsOn(typeof(AbpPermissionManagementApplicationModule))]
[DependsOn(typeof(AbpTenantManagementApplicationModule))]
[DependsOn(typeof(AbpFeatureManagementApplicationModule))]
[DependsOn(typeof(AbpSettingManagementApplicationModule))]
//app modules
[DependsOn(typeof(DynamicFormApplicationModule))]
public class MainDemoApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAutoMapperObjectMapper<MainDemoApplicationModule>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<MainDemoApplicationModule>();
        });
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddProfile<MainDemoApplicationAutoMapperProfile>(validate: true);
        });
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MainDemoApplicationModule>();
        });
    }
}
