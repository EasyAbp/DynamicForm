using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate;

public class EditModalModel : DynamicFormPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public EditFormTemplateViewModel ViewModel { get; set; }

    private readonly IFormTemplateAppService _service;

    public EditModalModel(IFormTemplateAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var dto = await _service.GetAsync(Id);
        ViewModel = ObjectMapper.Map<FormTemplateDto, EditFormTemplateViewModel>(dto);
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = ObjectMapper.Map<EditFormTemplateViewModel, UpdateFormTemplateDto>(ViewModel);
        await _service.UpdateAsync(Id, dto);
        return NoContent();
    }
}