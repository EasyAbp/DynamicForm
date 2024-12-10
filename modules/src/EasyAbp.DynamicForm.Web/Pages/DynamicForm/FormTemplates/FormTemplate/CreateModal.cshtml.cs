using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate;

public class CreateModalModel : DynamicFormPageModel
{
    [BindProperty]
    public CreateFormTemplateViewModel ViewModel { get; set; }

    public List<SelectListItem> FormDefinitionNames { get; set; }

    private readonly IFormTemplateAppService _service;
    private readonly IJsonSerializer _jsonSerializer;

    public CreateModalModel(
        IFormTemplateAppService service,
        IJsonSerializer jsonSerializer)
    {
        _service = service;
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task OnGetAsync()
    {
        var baseInfo = await _service.GetBaseInfoAsync();

        FormDefinitionNames = baseInfo.FormDefinitions
            .Select(x => new SelectListItem(x.DisplayName, x.Name)).ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormTemplateDto
        {
            FormDefinitionName = ViewModel.FormDefinitionName,
            Name = ViewModel.Name,
            CustomTag = ViewModel.CustomTag
        };

        await _service.CreateAsync(dto);

        return NoContent();
    }
}