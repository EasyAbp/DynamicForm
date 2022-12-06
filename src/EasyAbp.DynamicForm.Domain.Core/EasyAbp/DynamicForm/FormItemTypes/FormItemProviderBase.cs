using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes;

public abstract class FormItemProviderBase : IFormItemProvider
{
    protected IJsonSerializer JsonSerializer { get; }

    public FormItemProviderBase(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

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