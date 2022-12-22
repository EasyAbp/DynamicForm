using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.TextBox;

public class TextBoxFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "TextBox";
    public static string LocalizationItemKey { get; set; } = "FormItemType.TextBox";

    public TextBoxFormItemProvider(IJsonSerializer jsonSerializer) : base(jsonSerializer)
    {
    }

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        var configurations = GetConfigurations<TextBoxFormItemConfigurations>(metadata);

        if (configurations.MinLength.HasValue && configurations.MaxLength.HasValue &&
            configurations.MinLength > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidMaxLength);
        }

        if (!configurations.RegexPattern.IsNullOrWhiteSpace())
        {
            try
            {
                var _ = Regex.Match(string.Empty, configurations.RegexPattern!);
            }
            catch
            {
                throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidRegexPattern);
            }
        }

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        if (!metadata.Optional && value.IsNullOrWhiteSpace())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired);
        }

        if (metadata.AvailableValues.Any() && !metadata.AvailableValues.Contains(value))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        var configurations = GetConfigurations<TextBoxFormItemConfigurations>(metadata);

        if (configurations.MinLength.HasValue && !value.IsNullOrEmpty() && value!.Length < configurations.MinLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidValueLength);
        }

        if (configurations.MaxLength.HasValue && value?.Length > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidValueLength);
        }

        if (!value.IsNullOrWhiteSpace() && !configurations.RegexPattern.IsNullOrWhiteSpace() &&
            !Regex.IsMatch(value!, configurations.RegexPattern!))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new TextBoxFormItemConfigurations());
    }
}