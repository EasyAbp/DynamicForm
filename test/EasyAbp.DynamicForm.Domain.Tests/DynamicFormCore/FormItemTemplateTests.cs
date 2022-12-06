using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.BookRentals;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;
using EasyAbp.DynamicForm.FormTemplates;
using Shouldly;
using Volo.Abp;
using Volo.Abp.Json;
using Xunit;

namespace EasyAbp.DynamicForm.DynamicFormCore;

public class FormItemTemplateTests : DynamicFormDomainTestBase
{
    private readonly FormTemplateManager _formTemplateManager;
    private readonly IDynamicFormValidator _dynamicFormValidator;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IFormItemProviderResolver _formItemProviderResolver;

    public FormItemTemplateTests()
    {
        _formTemplateManager = GetRequiredService<FormTemplateManager>();
        _dynamicFormValidator = GetRequiredService<IDynamicFormValidator>();
        _jsonSerializer = GetRequiredService<IJsonSerializer>();
        _formItemProviderResolver = GetRequiredService<IFormItemProviderResolver>();
    }

    [Fact]
    public async Task Should_Validate_For_Custom_Template_Entities()
    {
        var textBoxFormItemProvider = GetFormItemProvider(TextBoxFormItemProvider.Name);

        var formItemTemplate1Configurations =
            (TextBoxFormItemConfigurations)await textBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        formItemTemplate1Configurations!.MinLength = 2;
        formItemTemplate1Configurations!.MaxLength = 1; // Wrong!

        var bookRental1 = new BookRental(Guid.NewGuid(), "Book1");

        var item = new BookRentalFormItem
        {
            BookRentalId = bookRental1.Id,
            Name = "Renter",
            Group = "MyGroup1",
            Type = TextBoxFormItemProvider.Name,
            Optional = false,
            Configurations = _jsonSerializer.Serialize(formItemTemplate1Configurations),
            DisplayOrder = 0,
            InfoText = "Book renter.",
            AvailableValues = null
        };

        await Should.ThrowAsync<BusinessException>(
            () => _dynamicFormValidator.ValidateTemplatesAsync(new[] { item }),
            "The max length should be greater than the min length.");

        formItemTemplate1Configurations!.MaxLength = 3; // Fixed!
        item.Configurations = _jsonSerializer.Serialize(formItemTemplate1Configurations);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateTemplatesAsync(new[] { item }));

        bookRental1.FormItems.Add(item);
    }

    [Fact]
    public async Task Should_Validate_TextBox()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(TextBoxFormItemProvider.Name);
        var configurations = (TextBoxFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        configurations!.MinLength = 2;
        configurations!.MaxLength = 1; // should not be less than the min length

        await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, TextBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0),
            "The max length should be greater than the min length.");

        configurations!.MaxLength = 3;
        configurations!.RegexPattern = "*****"; // invalid regex pattern

        await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, TextBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0),
            "Invalid regex pattern.");
    }

    [Fact]
    public async Task Should_Validate_OptionButtons()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(OptionButtonsFormItemProvider.Name);
        var configurations =
            (OptionButtonsFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        configurations!.IsMultiSelection = true;
        configurations!.MinSelection = 2;
        configurations!.MaxSelection = 1; // should not be less than the min selection

        await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, OptionButtonsFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0),
            "The max selection should be greater than the min selection.");
    }

    private async Task<FormTemplate> CreateFormTemplateAsync()
    {
        return await _formTemplateManager.CreateAsync(DynamicFormTestConsts.TestFormDefinitionName, "CustomForm", null);
    }

    private IFormItemProvider GetFormItemProvider(string name)
    {
        return _formItemProviderResolver.Resolve(name);
    }
}