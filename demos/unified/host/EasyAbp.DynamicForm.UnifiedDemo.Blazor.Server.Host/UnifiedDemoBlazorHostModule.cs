using System.IO;
using System.Threading.Tasks;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Volo.Abp;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme;
using Volo.Abp.AspNetCore.Components.Server.BasicTheme.Bundling;
using Volo.Abp.AspNetCore.Components.Web.Theming.Routing;
using Volo.Abp.AspNetCore.Mvc.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.Bundling;
using Volo.Abp.AspNetCore.Serilog;
using Volo.Abp.Autofac;
using Volo.Abp.Identity.Blazor.Server;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement.Blazor.Server;
using Volo.Abp.Swashbuckle;
using Volo.Abp.TenantManagement.Blazor.Server;
using Volo.Abp.UI.Navigation;
using Volo.Abp.UI.Navigation.Urls;
using Volo.Abp.VirtualFileSystem;
using EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server.Host.Menus;
using Volo.Abp.Account.Web;
using Volo.Abp.AspNetCore.Authentication.JwtBearer;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite;
using EasyAbp.DynamicForm.UnifiedDemo.EntityFrameworkCore;
using EasyAbp.DynamicForm.UnifiedDemo.Localization;
using EasyAbp.DynamicForm.UnifiedDemo.MultiTenancy;
using EasyAbp.DynamicForm.Options;
using Volo.Abp.AspNetCore.Mvc.UI.Theme.LeptonXLite.Bundling;
using OpenIddict.Validation.AspNetCore;

namespace EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server.Host;

[DependsOn(typeof(AbpAspNetCoreMvcUiLeptonXLiteThemeModule))]
[DependsOn(typeof(AbpAspNetCoreComponentsServerBasicThemeModule))]

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpSwashbuckleModule))]
[DependsOn(typeof(AbpAspNetCoreAuthenticationJwtBearerModule))]
[DependsOn(typeof(AbpAspNetCoreSerilogModule))]
[DependsOn(typeof(AbpIdentityBlazorServerModule))]
[DependsOn(typeof(AbpTenantManagementBlazorServerModule))]
[DependsOn(typeof(AbpSettingManagementBlazorServerModule))]

[DependsOn(typeof(UnifiedDemoHttpApiModule))]
[DependsOn(typeof(UnifiedDemoEntityFrameworkCoreModule))]
[DependsOn(typeof(UnifiedDemoApplicationModule))]

[DependsOn(typeof(AbpAccountWebModule))]
[DependsOn(typeof(AbpAccountWebOpenIddictModule))]

[DependsOn(typeof(UnifiedDemoBlazorServerModule))]
public class UnifiedDemoBlazorHostModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.PreConfigure<AbpMvcDataAnnotationsLocalizationOptions>(options =>
        {
            options.AddAssemblyResource(
                typeof(UnifiedDemoResource),
                typeof(UnifiedDemoDomainModule).Assembly,
                typeof(UnifiedDemoDomainSharedModule).Assembly,
                typeof(UnifiedDemoApplicationModule).Assembly,
                typeof(UnifiedDemoApplicationContractsModule).Assembly,
                typeof(UnifiedDemoBlazorHostModule).Assembly
            );
        });

        PreConfigure<OpenIddictBuilder>(builder =>
        {
            builder.AddValidation(options =>
            {
                options.AddAudiences("DynamicForm");
                options.UseLocalServer();
                options.UseAspNetCore();
            });
        });
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();

        context.Services.ForwardIdentityAuthenticationForBearer(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);

        Configure<DynamicFormOptions>(options =>
        {
            options.AddOrUpdateFormDefinition(new FormDefinition("InternalForm", "Internal Form"));
        });

        //Configure<AbpDbContextOptions>(options =>
        //{
        //    options.UseSqlServer();
        //});

        Configure<AbpBundlingOptions>(options =>
        {
            // MVC UI
            options.StyleBundles.Configure(
                LeptonXLiteThemeBundles.Styles.Global,
            bundle =>
            {
                bundle.AddFiles("/global-styles.css");
            });

            //BLAZOR UI
            options.StyleBundles.Configure(
            BlazorBasicThemeBundles.Styles.Global,
            bundle =>
            {
                bundle.AddFiles("/blazor-global-styles.css");
                //You can remove the following line if you don't use Blazor CSS isolation for components
                bundle.AddFiles("/EasyAbp.DynamicForm.UnifiedDemo.Blazor.Server.Host.styles.css");
            });
        });

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<DynamicFormDomainSharedModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}EasyAbp.DynamicForm.UnifiedDemo.Domain.Shared", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<DynamicFormDomainModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}EasyAbp.DynamicForm.UnifiedDemo.Domain", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<DynamicFormApplicationContractsModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}EasyAbp.DynamicForm.UnifiedDemo.Application.Contracts", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<DynamicFormApplicationModule>(Path.Combine(hostingEnvironment.ContentRootPath, string.Format("..{0}..{0}src{0}EasyAbp.DynamicForm.UnifiedDemo.Application", Path.DirectorySeparatorChar)));
                options.FileSets.ReplaceEmbeddedByPhysical<UnifiedDemoBlazorHostModule>(hostingEnvironment.ContentRootPath);
            });
        }

        context.Services.AddAbpSwaggerGen(
            options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "DynamicForm API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
                options.CustomSchemaIds(type => type.FullName);
            });

        Configure<AbpMultiTenancyOptions>(options =>
        {
            options.IsEnabled = MultiTenancyConsts.IsEnabled;
        });

        context.Services
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

        Configure<AbpNavigationOptions>(options =>
        {
            options.MenuContributors.Add(new UnifiedDemoMenuContributor());
        });

        Configure<AbpRouterOptions>(options =>
        {
            options.AppAssembly = typeof(UnifiedDemoBlazorHostModule).Assembly;
        });

        Configure<AppUrlOptions>(options =>
        {
            options.Applications["MVC"].RootUrl = configuration["App:SelfUrl"];
            options.RedirectAllowedUrls.AddRange(configuration["App:RedirectAllowedUrls"].Split(','));
        });
    }

    public async override Task OnApplicationInitializationAsync(ApplicationInitializationContext context)
    {
        var env = context.GetEnvironment();
        var app = context.GetApplicationBuilder();

        app.UseAbpRequestLocalization();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseCorrelationId();
        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAbpOpenIddictValidation();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "DynamicForm API");
        });
        app.UseConfiguredEndpoints();
    }
}
