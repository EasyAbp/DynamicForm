using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes;

public abstract class FormItemProviderBase : IFormItemProvider
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    protected IJsonSerializer JsonSerializer => LazyServiceProvider.LazyGetRequiredService<IJsonSerializer>();

    public abstract Task ValidateTemplateAsync(IFormItemMetadata metadata);

    public abstract Task ValidateValueAsync(IFormItemMetadata metadata, string value);

    public abstract Task<object> CreateConfigurationsObjectOrNullAsync();

    protected virtual TConfigurations GetConfigurations<TConfigurations>(IFormItemMetadata metadata)
    {
        return JsonSerializer.Deserialize<TConfigurations>(metadata.Configurations.IsNullOrWhiteSpace()
            ? "{}"
            : metadata.Configurations);
    }
}