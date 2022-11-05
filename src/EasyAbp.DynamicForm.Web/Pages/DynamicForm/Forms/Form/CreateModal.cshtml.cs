using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;
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

        ViewModel = new CreateFormViewModel
        {
            FormTemplateId = formTemplateId,
            FormItems = _jsonSerializer.Serialize(formTemplate.FormItemTemplates)
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