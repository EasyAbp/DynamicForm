using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form;

public class CreateModalModel : DynamicFormPageModel
{
    [BindProperty]
    public CreateFormViewModel ViewModel { get; set; }

    private readonly IFormAppService _service;
    private readonly IFormTemplateAppService _formTemplateAppService;
    private readonly IJsonSerializer _jsonSerializer;

    public CreateModalModel(
        IFormAppService service,
        IFormTemplateAppService formTemplateAppService,
        IJsonSerializer jsonSerializer)
    {
        _service = service;
        _formTemplateAppService = formTemplateAppService;
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task OnGetAsync(Guid formTemplateId)
    {
        var formTemplate = await _formTemplateAppService.GetAsync(formTemplateId);

        var formItemTemplatesString = _jsonSerializer.Serialize(formTemplate.FormItemTemplates);
        var formItemsString = _jsonSerializer.Serialize(formTemplate.FormItemTemplates.Select(x =>
            new CreateEditFormItemViewModel
            {
                Name = x.Name,
                Value = string.Empty
            }));

        var beautifiedFormItemTemplatesString = JToken.Parse(formItemTemplatesString).ToString(Formatting.Indented);
        var beautifiedFormItemsString = JToken.Parse(formItemsString).ToString(Formatting.Indented);

        ViewModel = new CreateFormViewModel
        {
            FormTemplateId = formTemplateId,
            FormItemTemplates = beautifiedFormItemTemplatesString,
            FormItems = beautifiedFormItemsString,
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormDto
        {
            FormTemplateId = ViewModel.FormTemplateId,
            FormItems = _jsonSerializer.Deserialize<List<CreateFormItemDto>>(ViewModel.FormItems)
        };

        await _service.CreateAsync(dto);

        return NoContent();
    }
}