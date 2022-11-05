using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Shared;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate;

public class CreateModalModel : DynamicFormPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    [BindProperty]
    public CreateFormItemTemplateViewModel ViewModel { get; set; }

    private readonly IFormTemplateAppService _service;
    private readonly IJsonSerializer _jsonSerializer;

    public CreateModalModel(
        IFormTemplateAppService service,
        IJsonSerializer jsonSerializer)
    {
        _service = service;
        _jsonSerializer = jsonSerializer;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormItemTemplateDto
        {
            Name = ViewModel.Name,
            Tip = ViewModel.Tip,
            Type = ViewModel.Type,
            Optional = ViewModel.Optional,
            RadioValues = new AvailableRadioValues(ViewModel.RadioValues.Split(','))
        };

        await _service.CreateFormItemAsync(FormTemplateId, dto);

        return NoContent();
    }
}