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

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<FormItemCreationModel>
            {
                new("Name", "John")
            }), "Missing required form item.");
    }

    [Fact]
    public async Task Should_Validate_Duplicate()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, new List<FormItemCreationModel>
            {
                new("Name", "John"),
                new("Name", "John")
            }), "Duplicate form item.");
    }

    [Fact]
    public async Task Should_Validate_TextBox()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = CreateStandardFormItems();

        formItems.First(x => x.Name == "Name").Value = null; // "Name" is not optional

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "The form item value is required.");

        formItems.First(x => x.Name == "Name").Value = "A"; // too short

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value length.");

        formItems.First(x => x.Name == "Name").Value = "Abcdefghijklmn"; // too long

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value length.");

        formItems.First(x => x.Name == "Name").Value = "John1"; // contains digital

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value.");

        formItems.First(x => x.Name == "Name").Value = "John";
        formItems.First(x => x.Name == "Dept").Value = "Dept 3"; // not in available values

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value.");
    }

    [Fact]
    public async Task Should_Validate_OptionButtons()
    {
        var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        var formItems = CreateStandardFormItems();

        formItems.First(x => x.Name == "Gender").Value = "Other"; // not in available values

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value length.");

        formItems.First(x => x.Name == "Gender").Value = "Male";
        formItems.First(x => x.Name == "Requirements").Value =
            "Use annual leave,Aha"; // "Aha" is not in available values

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item value length.");

        formItems.First(x => x.Name == "Requirements").Value = null; // min selection is 1

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item option quantity selected.");

        formItems.First(x => x.Name == "Requirements").Value =
            "Use annual leave,Urgent,Remote standby"; // max selection is 2

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "Invalid form item option quantity selected.");

        formItems.First(x => x.Name == "Requirements").Value = null; // "Requirements" is not optional

        await Should.ThrowAsync<BusinessException>(() => _dynamicFormValidator.ValidateValuesAsync(
            formTemplate.FormItemTemplates, formItems), "The form item value is required.");
    }

    private static List<FormItemCreationModel> CreateStandardFormItems()
    {
        return new List<FormItemCreationModel>
        {
            new("Name", "John"),
            new("Dept", "Dept 2"),
            new("Gender", "Male"),
            new("Requirements", "Use annual leave,Urgent"),
        };
    }
}