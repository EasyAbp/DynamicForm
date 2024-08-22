using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.BookRentals;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.FormItemTypes.ColorPicker;
using EasyAbp.DynamicForm.FormItemTypes.FileBox;
using EasyAbp.DynamicForm.FormItemTypes.NumberBox;
using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;
using EasyAbp.DynamicForm.FormItemTypes.TimePicker;
using EasyAbp.DynamicForm.FormItemTypes.Toggle;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
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
            InfoText = "Book renter.",
            Type = TextBoxFormItemProvider.Name,
            Optional = false,
            Configurations = _jsonSerializer.Serialize(formItemTemplate1Configurations),
            DisplayOrder = 0,
            AvailableValues = null
        };

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateTemplatesAsync(new[] { item })))
            .Code.ShouldBe("EasyAbp.DynamicForm:TextBoxInvalidMaxLength");

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

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, TextBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0, false)))
            .Code.ShouldBe("EasyAbp.DynamicForm:TextBoxInvalidMaxLength");

        configurations!.MaxLength = 3;
        configurations!.RegexPattern = "*****"; // invalid regex pattern

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, TextBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0, false)))
            .Code.ShouldBe("EasyAbp.DynamicForm:InvalidRegexPattern");
    }

    [Fact]
    public async Task Should_Validate_NumberBox()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(NumberBoxFormItemProvider.Name);
        var configurations = (NumberBoxFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        configurations!.MinValue = 2;
        configurations!.MaxValue = 1; // should not be less than the min value

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, NumberBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0, false)))
            .Code.ShouldBe("EasyAbp.DynamicForm:NumberBoxInvalidMaxValue");

        configurations!.MaxValue = 3;

        await Should.NotThrowAsync(() => _formTemplateManager.AddFormItemAsync(
            formTemplate, "Content", "group1", null, NumberBoxFormItemProvider.Name, false,
            _jsonSerializer.Serialize(configurations), null, 0, false));

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, NumberBoxFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), ["abc"], 0, false))) // abc is not a numeric value
            .Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");
    }

    [Fact]
    public async Task Should_Validate_Toggle()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(ToggleFormItemProvider.Name);
        var configurations = (ToggleFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        const bool optional = true; // cannot be optional.

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, ToggleFormItemProvider.Name, optional,
                _jsonSerializer.Serialize(configurations!), null, 0, false)))
            .Code.ShouldBe("EasyAbp.DynamicForm:ToggleIsOptional");

        AvailableValues availableValues = ["abc"]; // valid available values: true, false.

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, ToggleFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations!), availableValues, 0, false))) // abc is not a numeric value
            .Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        await Should.NotThrowAsync(() => _formTemplateManager.AddFormItemAsync(
            formTemplate, "Content", "group1", null, ToggleFormItemProvider.Name, false,
            _jsonSerializer.Serialize(configurations!), null, 0, false));
    }

    [Fact]
    public async Task Should_Validate_TimePicker()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(TimePickerFormItemProvider.Name);
        var configurations = (TimePickerFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        AvailableValues availableValues = ["abc"]; // invalid date time value

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, TimePickerFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations!), availableValues, 0, false))) // abc is not a numeric value
            .Code.ShouldBe("EasyAbp.DynamicForm:TimePickerInvalidDateTime");

        await Should.NotThrowAsync(() => _formTemplateManager.AddFormItemAsync(
            formTemplate, "Content", "group1", null, TimePickerFormItemProvider.Name, false,
            _jsonSerializer.Serialize(configurations!), null, 0, false));
    }

    [Fact]
    public async Task Should_Validate_ColorPicker()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(ColorPickerFormItemProvider.Name);
        var configurations = (ColorPickerFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        AvailableValues availableValues = ["abc"]; // invalid color value

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, ColorPickerFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations!), availableValues, 0, false))) // abc is not a numeric value
            .Code.ShouldBe("EasyAbp.DynamicForm:ColorPickerInvalidHexValue");

        configurations!.RegexPattern = "*****"; // invalid regex pattern

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, ColorPickerFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations!), null, 0, false))) // abc is not a numeric value
            .Code.ShouldBe("EasyAbp.DynamicForm:InvalidRegexPattern");

        configurations!.RegexPattern = null;

        await Should.NotThrowAsync(() => _formTemplateManager.AddFormItemAsync(
            formTemplate, "Content", "group1", null, ColorPickerFormItemProvider.Name, false,
            _jsonSerializer.Serialize(configurations!), null, 0, false));
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

        (await Should.ThrowAsync<BusinessException>(() => _formTemplateManager.AddFormItemAsync(
                formTemplate, "Content", "group1", null, OptionButtonsFormItemProvider.Name, false,
                _jsonSerializer.Serialize(configurations), null, 0, false)))
            .Code.ShouldBe("EasyAbp.DynamicForm:OptionButtonsInvalidMaxSelection");
    }

    [Fact]
    public async Task Should_Validate_FileBox()
    {
        var formTemplate = await CreateFormTemplateAsync();
        var provider = GetFormItemProvider(FileBoxFormItemProvider.Name);
        var configurations = (FileBoxFormItemConfigurations)await provider.CreateConfigurationsObjectOrNullAsync();

        configurations!.ProviderName = "EasyAbpFileManagement"; // module name
        configurations!.ProviderKey = "UserUpload"; // container name

        // This module doesn't validate the provider info. If you want it, implement it on the app side.
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