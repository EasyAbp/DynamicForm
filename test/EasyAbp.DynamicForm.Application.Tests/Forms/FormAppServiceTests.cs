namespace EasyAbp.DynamicForm.Forms;

public class FormAppServiceTests : DynamicFormApplicationTestBase
{
    private readonly IFormAppService _formAppService;

    public FormAppServiceTests()
    {
        _formAppService = GetRequiredService<IFormAppService>();
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

