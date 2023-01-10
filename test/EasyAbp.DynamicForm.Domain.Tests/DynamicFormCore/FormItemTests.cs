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
    public async Task Should_Validate_Quantity()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<FormItemCreationModel>
            {
                new("Name", "John")
            }))).Code.ShouldBe("EasyAbp.DynamicForm:MissingFormItem");
    }

    [Fact]
    public async Task Should_Validate_Duplicate()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        (await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<FormItemCreationModel>
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