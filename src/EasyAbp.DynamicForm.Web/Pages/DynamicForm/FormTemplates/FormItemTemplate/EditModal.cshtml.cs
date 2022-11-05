using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Shared;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate;

public class EditModalModel : DynamicFormPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public string Name { get; set; }

    [BindProperty]
    public EditFormItemTemplateViewModel ViewModel { get; set; }

    private readonly IFormTemplateAppService _service;

    public EditModalModel(IFormTemplateAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(FormTemplateId);
        var item = dto.FormItemTemplates.First(x => x.Name == Name);

        ViewModel = new EditFormItemTemplateViewModel
        {
            InfoText = item.InfoText,
            Type = item.Type,
            Optional = item.Optional,
            RadioValues = item.RadioValues.JoinAsString(",")
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new UpdateFormItemTemplateDto
        {
            InfoText = ViewModel.InfoText,
            Type = ViewModel.Type,
            Optional = ViewModel.Optional,
            RadioValues = new AvailableRadioValues(ViewModel.RadioValues.Split(','))
        };

        await _service.UpdateFormItemAsync(FormTemplateId, Name, dto);

        return NoContent();
    }
}