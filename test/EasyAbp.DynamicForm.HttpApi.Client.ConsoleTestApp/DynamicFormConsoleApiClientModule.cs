using Volo.Abp.Autofac;
using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace EasyAbp.DynamicForm;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DynamicFormHttpApiClientModule),
    typeof(AbpHttpClientIdentityModelModule)
    )]
public class DynamicFormConsoleApiClientModule : AbpModule
{

}
