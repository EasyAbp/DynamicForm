using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form;

public class EditModalModel : DynamicFormPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditFormViewModel ViewModel { get; set; }

    private readonly IFormAppService _service;
    private readonly IJsonSerializer _jsonSerializer;

    public EditModalModel(
        IFormAppService service,
        IJsonSerializer jsonSerializer)
    {
        _service = service;
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);

        ViewModel = new EditFormViewModel
        {
            FormItems = _jsonSerializer.Serialize(dto.FormItems)
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var items = _jsonSerializer.Deserialize<List<FormItemDto>>(ViewModel.FormItems);

        foreach (var item in items)
        {
            await _service.UpdateFormItemAsync(Id, item.Name, new UpdateFormItemDto { Value = item.Value });
        }

        return NoContent();
    }
}