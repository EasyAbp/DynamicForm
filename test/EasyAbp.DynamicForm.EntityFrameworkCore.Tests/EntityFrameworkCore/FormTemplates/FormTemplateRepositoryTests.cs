using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.DynamicForm.EntityFrameworkCore.FormTemplates;

public class FormTemplateRepositoryTests : DynamicFormEntityFrameworkCoreTestBase
{
    private readonly IFormTemplateRepository _formTemplateRepository;

    public FormTemplateRepositoryTests()
    {
        _formTemplateRepository = GetRequiredService<IFormTemplateRepository>();
    }

    /*
    [Fact]
    public async Task Test1()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            // Arrange

            // Act

            //Assert
        });
    }
    */
}
