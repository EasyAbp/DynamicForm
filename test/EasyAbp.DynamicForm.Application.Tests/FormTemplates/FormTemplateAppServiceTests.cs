namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateAppServiceTests : DynamicFormApplicationTestBase
{
    private readonly IFormTemplateAppService _formTemplateAppService;

    public FormTemplateAppServiceTests()
    {
        _formTemplateAppService = GetRequiredService<IFormTemplateAppService>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        // Arrange

        // Act

        // Assert
    }
    */
}

