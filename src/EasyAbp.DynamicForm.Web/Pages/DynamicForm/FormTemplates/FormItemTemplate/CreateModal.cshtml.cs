using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Shared;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate;

public class CreateModalModel : DynamicFormPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    [BindProperty(SupportsGet = true)]
    public CreateFormItemTemplateViewModel ViewModel { get; set; }

    public List<SelectListItem> FormItemTypes { get; set; }

    private readonly IFormTemplateAppService _service;

    public CreateModalModel(IFormTemplateAppService service)
    {
        _service = service;
    }

    public virtual async Task OnGetAsync()
    {
        var baseInfo = await _service.GetBaseInfoAsync();

        var configurationsJsonTemplate = baseInfo.FormItemTypeDefinitions.First(x => x.Name == ViewModel.Type)
            .ConfigurationsJsonTemplate;

        var beautifiedConfigurations = JToken.Parse(configurationsJsonTemplate).ToString(Formatting.Indented);

        ViewModel = new CreateFormItemTemplateViewModel
        {
            Configurations = beautifiedConfigurations
        };

        FormItemTypes = baseInfo.FormItemTypeDefinitions
            .Select(x => new SelectListItem(L[x.LocalizationItemKey], x.Name)).ToList();
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormItemTemplateDto
        {
            Name = ViewModel.Name,
            Group = ViewModel.Group,
            InfoText = ViewModel.InfoText,
            Type = ViewModel.Type,
            Optional = ViewModel.Optional,
            Configurations = ViewModel.Configurations,
            AvailableValues = new AvailableValues(ViewModel.AvailableValues.Split(',')),
            DisplayOrder = ViewModel.DisplayOrder
        };

        await _service.CreateFormItemAsync(FormTemplateId, dto);

        return NoContent();
    }
}