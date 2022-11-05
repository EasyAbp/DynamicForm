using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms;
using Volo.Abp.Domain.Repositories;
using Xunit;

namespace EasyAbp.DynamicForm.EntityFrameworkCore.Forms;

public class FormRepositoryTests : DynamicFormEntityFrameworkCoreTestBase
{
    private readonly IFormRepository _formRepository;

    public FormRepositoryTests()
    {
        _formRepository = GetRequiredService<IFormRepository>();
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
