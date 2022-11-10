using System;
using System.Linq;
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
    public static string LocalizationItemKey { get; set; } = "FormItemType.OptionButtons";
    public static string SelectionSeparator { get; set; } = ",";

    private readonly IJsonSerializer _jsonSerializer;

    public OptionButtonsFormItemProvider(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual Task ValidateTemplateAsync(IFormItemTemplate formItemTemplate)
    {
        var configurations = GetConfigurations(formItemTemplate);

        if (configurations.MinSelection.HasValue && configurations.MaxSelection.HasValue &&
            configurations.MinSelection > configurations.MaxSelection)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.OptionButtonsInvalidMaxSelection);
        }

        return Task.CompletedTask;
    }

    public virtual Task ValidateValueAsync(IFormItemMetadata formItemMetadata, string value)
    {
        if (!formItemMetadata.Optional && value.IsNullOrWhiteSpace())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired);
        }

        if (formItemMetadata.AvailableValues.Any() && !formItemMetadata.AvailableValues.Contains(value))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        var configurations = GetConfigurations(formItemMetadata);
        var selections = configurations.IsMultiSelection ? value.Split(SelectionSeparator) : new[] { value };

        if (configurations.IsMultiSelection && configurations.MinSelection.HasValue &&
            selections.Length < configurations.MinSelection)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.OptionButtonsInvalidOptionQuantitySelected);
        }

        if (configurations.IsMultiSelection && configurations.MaxSelection.HasValue &&
            selections.Length > configurations.MaxSelection)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.OptionButtonsInvalidOptionQuantitySelected);
        }

        return Task.CompletedTask;
    }

    public virtual Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new OptionButtonsFormItemConfigurations());
    }

    protected virtual OptionButtonsFormItemConfigurations GetConfigurations(IFormItemMetadata formItemTemplate)
    {
        return _jsonSerializer.Deserialize<OptionButtonsFormItemConfigurations>(formItemTemplate.Configurations);
    }
}