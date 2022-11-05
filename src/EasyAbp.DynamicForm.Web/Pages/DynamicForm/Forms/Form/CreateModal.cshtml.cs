using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form;

public class CreateModalModel : DynamicFormPageModel
{
    [BindProperty]
    public CreateFormViewModel ViewModel { get; set; }

    private readonly IFormAppService _service;
    private readonly IJsonSerializer _jsonSerializer;

    public CreateModalModel(
        IFormAppService service,
        IJsonSerializer jsonSerializer)
    {
        _service = service;
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task OnGetAsync(Guid formTemplateId)
    {
        ViewModel = new CreateFormViewModel
        {
            FormDefinitionName = null, // todo: use FormTemplate app service
            FormTemplateId = formTemplateId,
            FormItems = null // todo: use FormTemplate app service
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormDto
        {
            FormDefinitionName = ViewModel.FormDefinitionName,
            FormTemplateId = ViewModel.FormTemplateId,
            FormItems = _jsonSerializer.Deserialize<List<CreateFormItemDto>>(ViewModel.FormItems)
        };

        await _service.CreateAsync(dto);

        return NoContent();
    }
}