using System;
using System.Linq;
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

    [BindProperty(SupportsGet = true)]
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

    public virtual async Task OnGetAsync()
    {
        var baseInfo = await _service.GetBaseInfoAsync();

        ViewModel = new CreateFormItemTemplateViewModel
        {
            Configurations =
                _jsonSerializer.Serialize(baseInfo.FormItemTypeDefinitions.First(x => x.Name == ViewModel.Type))
        };
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        var dto = new CreateFormItemTemplateDto
        {
            Name = ViewModel.Name,
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