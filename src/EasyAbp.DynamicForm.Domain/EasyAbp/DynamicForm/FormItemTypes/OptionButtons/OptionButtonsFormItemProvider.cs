using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.OptionButtons;

public class OptionButtonsFormItemProvider : IFormItemProvider, IScopedDependency
{
    public static string Name { get; set; } = "OptionButtons";
    public static string LocalizationItemKey { get; set; } = "OptionButtons";
    public static string SelectionSeparator { get; set; } = ",";

    private readonly IJsonSerializer _jsonSerializer;

    public OptionButtonsFormItemProvider(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual Task ValidateFormItemTemplateAsync(FormItemTemplate formItemTemplate)
    {
        var configurations = GetConfigurations(formItemTemplate);

        if (configurations.MinSelection.HasValue && configurations.MaxSelection.HasValue &&
            configurations.MinSelection > configurations.MaxSelection)
        {
            throw new BusinessException(DynamicFormErrorCodes.OptionButtonsInvalidMaxSelection);
        }

        return Task.CompletedTask;
    }

    public virtual Task ValidateFormItemValueAsync(IFormItemMetadata formItemMetadata, string value)
    {
        var selections = value.Split(SelectionSeparator);
        var configurations = GetConfigurations(formItemMetadata);

        if (configurations.MinSelection.HasValue && selections.Length < configurations.MinSelection)
        {
            throw new BusinessException(DynamicFormErrorCodes.OptionButtonsInvalidOptionQuantitySelected);
        }

        if (configurations.MaxSelection.HasValue && selections.Length > configurations.MaxSelection)
        {
            throw new BusinessException(DynamicFormErrorCodes.OptionButtonsInvalidOptionQuantitySelected);
        }

        return Task.CompletedTask;
    }

    public virtual Task<object> CreateFormItemConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new OptionButtonsFormItemConfigurations());
    }

    protected virtual OptionButtonsFormItemConfigurations GetConfigurations(IFormItemMetadata formItemTemplate)
    {
        return _jsonSerializer.Deserialize<OptionButtonsFormItemConfigurations>(formItemTemplate.Configurations);
    }
}