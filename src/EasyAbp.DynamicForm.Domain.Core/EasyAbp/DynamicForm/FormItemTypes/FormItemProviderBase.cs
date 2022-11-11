using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
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

    public abstract Task ValidateTemplateAsync(IFormItemTemplate formItemTemplate);

    public abstract Task ValidateValueAsync(IFormItemMetadata formItemMetadata, string value);

    public abstract Task<object> CreateConfigurationsObjectOrNullAsync();

    protected virtual TConfigurations GetConfigurations<TConfigurations>(IFormItemMetadata formItemTemplate)
    {
        return JsonSerializer.Deserialize<TConfigurations>(formItemTemplate.Configurations.IsNullOrWhiteSpace()
            ? "{}"
            : formItemTemplate.Configurations);
    }
}