using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using Shouldly;
using Xunit;

namespace EasyAbp.DynamicForm.Forms;

public class FormDomainTests : DynamicFormDomainTestBase
{
    private readonly IFormTemplateRepository _formTemplateRepository;
    private readonly FormManager _formManager;

    public FormDomainTests()
    {
        _formTemplateRepository = GetRequiredService<IFormTemplateRepository>();
        _formManager = GetRequiredService<FormManager>();
    }

    [Fact]
    public async Task Should_Create_Form()
    {
        await WithUnitOfWorkAsync(async () =>
        {
            var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);
            var form = await _formManager.CreateAsync(formTemplate, new List<FormItemCreationModel>
            {
                new("Name", "John"),
                new("Dept", "Dept 2"),
                new("Gender", "Male"),
                new("Requirements", "Use annual leave,Urgent"),
            });

            form.FormItems.Count.ShouldBe(4);
            form.FormItems.ShouldContain(x => x.Name == "Name" && x.Value == "John");
            form.FormItems.ShouldContain(x => x.Name == "Dept" && x.Value == "Dept 2");
            form.FormItems.ShouldContain(x => x.Name == "Gender" && x.Value == "Male");
            form.FormItems.ShouldContain(x => x.Name == "Requirements" && x.Value == "Use annual leave,Urgent");
        });
    }
}