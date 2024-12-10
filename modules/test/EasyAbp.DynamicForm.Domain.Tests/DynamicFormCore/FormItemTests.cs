using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.FormTemplates;
using Shouldly;
using Volo.Abp;
using Xunit;

namespace EasyAbp.DynamicForm.DynamicFormCore;

public class FormItemTests : DynamicFormDomainTestBase
{
    private readonly IDynamicFormValidator _dynamicFormValidator;
    private readonly IFormTemplateRepository _formTemplateRepository;

    public FormItemTests()
    {
        _dynamicFormValidator = GetRequiredService<IDynamicFormValidator>();
        _formTemplateRepository = GetRequiredService<IFormTemplateRepository>();
    }

    [Fact]
    public async Task Should_Pass_Validation()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<CreateUpdateFormItemModel>
            {
                new("Name", "John"),
                new("DisabledTextBox", ""), // this item is NOT optional, but disabled
                new("Dept", "Dept 1"),
                new("Gender", "Male"),
                new("Requirements", "Urgent"),
                new("Images", "[]"),
                new("Score", "100"),
                new("VIP", "false"),
                new("Birthday", "2024-08-21"),
                new("Color", "#FFAABBCC")
            }));
    }

    [Fact]
    public async Task Should_Validate_Quantity()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<CreateUpdateFormItemModel>
            {
                new("Name", "John")
            }))).Code.ShouldBe("EasyAbp.DynamicForm:MissingFormItem");
    }

    [Fact]
    public async Task Should_Validate_Duplicate()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<CreateUpdateFormItemModel>
            {
                new("Name", "John"),
                new("Name", "John")
            }))).Code.ShouldBe("EasyAbp.DynamicForm:DuplicateFormItem");
    }

    [Fact]
    public async Task Should_Validate_TextBox()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Name").Value = null; // "Name" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        var textBoxFormItemTemplate = formTemplate.FormItemTemplates.First(x => x.Name == "Name");
        textBoxFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(textBoxFormItemTemplate, true);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems)); // "Name" is null but optional

        textBoxFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(textBoxFormItemTemplate, false);

        formItems.First(x => x.Name == "Name").Value = "A"; // too short

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:TextBoxInvalidValueLength");

        formItems.First(x => x.Name == "Name").Value = "Abcdefghijklmn"; // too long

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:TextBoxInvalidValueLength");

        formItems.First(x => x.Name == "Name").Value = "John1"; // contains digital

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Name").Value = "John";
        formItems.First(x => x.Name == "Dept").Value = "Dept 3"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");
    }

    [Fact]
    public async Task Should_Validate_NumberBox()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Score").Value = null; // "Score" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        var numberBoxFormItemTemplate = formTemplate.FormItemTemplates.First(x => x.Name == "Score");
        numberBoxFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(numberBoxFormItemTemplate, true);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems)); // "Score" is null but optional

        numberBoxFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(numberBoxFormItemTemplate, false);

        formItems.First(x => x.Name == "Score").Value = "9"; // too small

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:NumberBoxInvalidValue");

        formItems.First(x => x.Name == "Score").Value = "999"; // too large

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:NumberBoxInvalidValue");

        formItems.First(x => x.Name == "Score").Value = "100.111"; // decimal places > 2

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:NumberBoxInvalidValue");

        formItems.First(x => x.Name == "Score").Value = "101"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Score").Value = "100.11";

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));
    }

    [Fact]
    public async Task Should_Validate_Toggle()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "VIP").Value = null; // "VIP" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        formItems.First(x => x.Name == "VIP").Value = "abc"; // not bool value

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "VIP").Value = "true"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "VIP").Value = "false";

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));
    }

    [Fact]
    public async Task Should_Validate_TimePicker()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Birthday").Value = null; // "Birthday" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        var timePickerFormItemTemplate = formTemplate.FormItemTemplates.First(x => x.Name == "Birthday");
        timePickerFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(timePickerFormItemTemplate, true);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems)); // "Birthday" is null but optional

        formItems.First(x => x.Name == "Birthday").Value = "abc"; // invalid date time value

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:TimePickerInvalidDateTime");

        formItems.First(x => x.Name == "Birthday").Value = "2024-08-20"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Birthday").Value = "2024-08-21";

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));
    }

    [Fact]
    public async Task Should_Validate_ColorPicker()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Color").Value = null; // "Color" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        var colorPickerFormItemTemplate = formTemplate.FormItemTemplates.First(x => x.Name == "Color");
        colorPickerFormItemTemplate.GetType().GetProperty("Optional")!.SetValue(colorPickerFormItemTemplate, true);

        await Should.NotThrowAsync(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems)); // "Color" is null but optional

        formItems.First(x => x.Name == "Color").Value = "abc"; // invalid HEX value

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
                formTemplate.FormItemTemplates, formItems))).Code
            .ShouldBe("EasyAbp.DynamicForm:ColorPickerInvalidHexValue");

        formItems.First(x => x.Name == "Color").Value =
            "#000"; // should not contain 0-9 (Color property regex: "^[^0-9]*$")

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Color").Value = "#fff"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Color").Value = "#FFAABBCC";

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));
    }

    [Fact]
    public async Task Should_Validate_OptionButtons()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Gender").Value = "Other"; // not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Gender").Value = "Male";
        formItems.First(x => x.Name == "Requirements").Value =
            "Use annual leave,Aha"; // "Aha" is not in available values

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:InvalidFormItemValue");

        formItems.First(x => x.Name == "Gender").Value = null; // "Gender" is not optional

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FormItemValueIsRequired");

        formItems.First(x => x.Name == "Gender").Value = "Male";
        formItems.First(x => x.Name == "Requirements").Value = null; // min selection is 1

        (await Should.ThrowAsync<BusinessException>(() =>
                _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems)))
            .Code.ShouldBe("EasyAbp.DynamicForm:OptionButtonsInvalidOptionQuantitySelected");

        formItems.First(x => x.Name == "Requirements").Value =
            "Use annual leave,Urgent,Remote standby"; // max selection is 2

        (await Should.ThrowAsync<BusinessException>(() =>
                _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems)))
            .Code.ShouldBe("EasyAbp.DynamicForm:OptionButtonsInvalidOptionQuantitySelected");
    }

    [Fact]
    public async Task Should_Validate_FileBox()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = FormItemTestHelper.CreateStandardFormItems();

        formItems.First(x => x.Name == "Images").Value =
            "[\"https://my-fake-site1.com/1.png\", \"ftp://my-fake-site1.com/2.png\"]"; // contains a ftp scheme

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems))).Code.ShouldBe("EasyAbp.DynamicForm:FileBoxInvalidUrls");

        formItems.First(x => x.Name == "Images").Value = null; // null value

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));

        formItems.First(x => x.Name == "Images").Value = ""; // empty value

        await Should.NotThrowAsync(() =>
            _dynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, formItems));
    }
}