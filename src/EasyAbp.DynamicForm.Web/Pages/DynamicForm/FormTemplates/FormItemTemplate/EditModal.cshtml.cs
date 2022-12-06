using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Shared;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        var beautifiedConfigurations = JToken.Parse(item.Configurations).ToString(Formatting.Indented);

        ViewModel = new EditFormItemTemplateViewModel
        {
            Group = item.Group,
            InfoText = item.InfoText,
            Type = item.Type,
            Optional = item.Optional,
            Configurations = beautifiedConfigurations,
            AvailableValues = item.AvailableValues.JoinAsString(","),
            DisplayOrder = item.DisplayOrder
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new UpdateFormItemTemplateDto
        {
            Group = ViewModel.Group,
            InfoText = ViewModel.InfoText,
            Type = ViewModel.Type,
            Optional = ViewModel.Optional,
            Configurations = ViewModel.Configurations,
            AvailableValues = new AvailableValues(ViewModel.AvailableValues.Split(',')),
            DisplayOrder = ViewModel.DisplayOrder
        };

        await _service.UpdateFormItemAsync(FormTemplateId, Name, dto);

        return NoContent();
    }
}