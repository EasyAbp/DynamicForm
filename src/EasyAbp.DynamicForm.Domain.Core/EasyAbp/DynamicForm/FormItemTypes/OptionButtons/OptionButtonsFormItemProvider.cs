using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.OptionButtons;

public class OptionButtonsFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "OptionButtons";
    public static string LocalizationItemKey { get; set; } = "FormItemType.OptionButtons";
    public static string SelectionSeparator { get; set; } = ",";

    public OptionButtonsFormItemProvider(IJsonSerializer jsonSerializer) : base(jsonSerializer)
    {
    }

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        var configurations = GetConfigurations<OptionButtonsFormItemConfigurations>(metadata);

        if (configurations.MinSelection.HasValue && configurations.MaxSelection.HasValue &&
            configurations.MinSelection > configurations.MaxSelection)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.OptionButtonsInvalidMaxSelection);
        }

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        if (!metadata.Optional && value.IsNullOrWhiteSpace())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired);
        }

        value ??= string.Empty;

        var configurations = GetConfigurations<OptionButtonsFormItemConfigurations>(metadata);

        var selections = configurations.IsMultiSelection
            ? value.Split(SelectionSeparator, StringSplitOptions.RemoveEmptyEntries)
            : new[] { value };

        if (selections.Any(selection =>
                metadata.AvailableValues.Any() && !metadata.AvailableValues.Contains(selection)))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

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

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new OptionButtonsFormItemConfigurations());
    }
}