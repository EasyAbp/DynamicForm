using Shouldly;
using System.Threading.Tasks;
using Xunit;

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

