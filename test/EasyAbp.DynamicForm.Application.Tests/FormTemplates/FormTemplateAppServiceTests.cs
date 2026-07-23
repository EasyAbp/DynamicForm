using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using Shouldly;
using Xunit;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateAppServiceTests : DynamicFormApplicationTestBase
{
    private readonly IFormTemplateAppService _formTemplateAppService;

    public FormTemplateAppServiceTests()
    {
        _formTemplateAppService = GetRequiredService<IFormTemplateAppService>();
    }

    [Fact]
    public async Task Should_Map_FormTemplate_Entity_To_Dto()
    {
        // Act
        var dto = await _formTemplateAppService.GetAsync(DynamicFormTestConsts.FormTemplate1Id);

        // Assert
        dto.ShouldNotBeNull();
        dto.Id.ShouldBe(DynamicFormTestConsts.FormTemplate1Id);
        dto.FormDefinitionName.ShouldBe(DynamicFormTestConsts.TestFormDefinitionName);
        dto.Name.ShouldBe(DynamicFormTestConsts.FormTemplate1Name);
        dto.CustomTag.ShouldBe("my-custom-tag");

        // Nested collection mapping (FormItemTemplate -> FormItemTemplateDto).
        dto.FormItemTemplates.ShouldNotBeNull();
        dto.FormItemTemplates.ShouldNotBeEmpty();

        var nameItem = dto.FormItemTemplates.Single(x => x.Name == "Name");
        nameItem.FormTemplateId.ShouldBe(DynamicFormTestConsts.FormTemplate1Id);
        nameItem.Group.ShouldBe("group1");
        nameItem.InfoText.ShouldBe("Your full name.");
        nameItem.Optional.ShouldBeFalse();

        var disabledItem = dto.FormItemTemplates.Single(x => x.Name == "DisabledTextBox");
        disabledItem.Disabled.ShouldBeTrue();
    }
}
