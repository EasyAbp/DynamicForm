using System;
using EasyAbp.DynamicForm.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes;

public class FormItemProviderResolver : IFormItemProviderResolver, IScopedDependency
{
    protected IServiceProvider ServiceProvider { get; }
    protected DynamicFormCoreOptions Options { get; }

    public FormItemProviderResolver(
        IServiceProvider serviceProvider,
        IOptions<DynamicFormCoreOptions> options)
    {
        ServiceProvider = serviceProvider;
        Options = options.Value;
    }

    public IFormItemProvider Resolve(string providerName)
    {
        return (IFormItemProvider)ServiceProvider.GetRequiredService(
            Options.GetFormItemTypeDefinition(providerName).ProviderType);
    }
}