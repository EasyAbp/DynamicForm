using System.Collections.Generic;
using System.Linq;
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
            var creatingFormItems = FormItemTestHelper.CreateStandardFormItems();

            var formTemplate = await _formTemplateRepository.GetAsync(DynamicFormTestConsts.FormTemplate1Id);
            var form = await _formManager.CreateAsync(formTemplate, creatingFormItems);

            form.FormItems.Count.ShouldBe(creatingFormItems.Count - 1);

            foreach (var creatingFormItem in creatingFormItems.Where(x => x.Name != "DisabledTextBox"))
            {
                form.FormItems.ShouldContain(x => x.Name == creatingFormItem.Name && x.Value == creatingFormItem.Value);
            }
        });
    }
}