using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes.TextBox;

public class TextBoxFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "TextBox";
    public static string LocalizationItemKey { get; set; } = "FormItemType.TextBox";

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        var configurations = GetConfigurations<TextBoxFormItemConfigurations>(metadata);

        if (configurations.MinLength.HasValue && configurations.MaxLength.HasValue &&
            configurations.MinLength > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidMaxLength)
                .WithData("item", metadata.Name);
        }

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

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        var isEmptyValue = value.IsNullOrWhiteSpace();

        if (!metadata.Optional && isEmptyValue)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired)
                .WithData("item", metadata.Name);
        }

        var configurations = GetConfigurations<TextBoxFormItemConfigurations>(metadata);

        if (configurations.MinLength.HasValue && !isEmptyValue && value.Length < configurations.MinLength ||
            (configurations.MaxLength.HasValue && !isEmptyValue && value.Length > configurations.MaxLength))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidValueLength)
                .WithData("item", metadata.Name)
                .WithData("min", configurations.MinLength ?? 0)
                .WithData("max", configurations.MaxLength.HasValue ? configurations.MaxLength.Value : "∞");
        }

        if (!isEmptyValue && !configurations.RegexPattern.IsNullOrWhiteSpace() &&
            !Regex.IsMatch(value!, configurations.RegexPattern!))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        if (!isEmptyValue && metadata.AvailableValues.Any() && !metadata.AvailableValues.Contains(value))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new TextBoxFormItemConfigurations());
    }
}