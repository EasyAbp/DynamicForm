using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        var formItemTemplatesString = _jsonSerializer.Serialize(dto.FormItems);
        var formItemsString = _jsonSerializer.Serialize(dto.FormItems.Select(x => new CreateEditFormItemViewModel
        {
            Name = x.Name,
            Value = x.Value
        }));

        var beautifiedFormItemTemplatesString = JToken.Parse(formItemTemplatesString).ToString(Formatting.Indented);
        var beautifiedFormItemsString = JToken.Parse(formItemsString).ToString(Formatting.Indented);

        ViewModel = new EditFormViewModel
        {
            FormItemTemplates = beautifiedFormItemTemplatesString,
            FormItems = beautifiedFormItemsString,
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