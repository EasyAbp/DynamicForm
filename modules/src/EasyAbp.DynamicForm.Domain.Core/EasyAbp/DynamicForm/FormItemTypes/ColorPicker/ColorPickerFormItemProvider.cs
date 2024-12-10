using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes.ColorPicker;

public partial class ColorPickerFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "ColorPicker";
    public static string LocalizationItemKey { get; set; } = "FormItemType.ColorPicker";

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        var configurations = GetConfigurations<ColorPickerFormItemConfigurations>(metadata);

        if (!configurations.RegexPattern.IsNullOrWhiteSpace())
        {
            try
            {
                _ = Regex.Match(string.Empty, configurations.RegexPattern!);
            }
            catch
            {
                throw new BusinessException(DynamicFormCoreErrorCodes.InvalidRegexPattern)
                    .WithData("item", metadata.Name);
            }
        }

        if (metadata.AvailableValues.Any(x => !HexRegex().IsMatch(x)))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.ColorPickerInvalidHexValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        var isEmptyValue = value.IsNullOrWhiteSpace();
        var configurations = GetConfigurations<ColorPickerFormItemConfigurations>(metadata);

        if (!metadata.Optional && isEmptyValue)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired)
                .WithData("item", metadata.Name);
        }

        if (isEmptyValue)
        {
            return Task.CompletedTask;
        }

        if (!HexRegex().IsMatch(value))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.ColorPickerInvalidHexValue)
                .WithData("item", metadata.Name);
        }

        if (!configurations.RegexPattern.IsNullOrWhiteSpace() && !Regex.IsMatch(value!, configurations.RegexPattern!))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        if (metadata.AvailableValues.Any() &&
            !metadata.AvailableValues.Contains(value, StringComparer.InvariantCultureIgnoreCase))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new ColorPickerFormItemConfigurations());
    }

    [GeneratedRegex("^#(?:[0-9a-fA-F]{3,4}){1,2}$")]
    private static partial Regex HexRegex();
}