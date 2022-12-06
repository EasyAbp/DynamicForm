using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate;

public class IndexModel : DynamicFormPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    public List<FormItemTypeDefinitionDto> FormItemTypes { get; set; }

    private readonly IFormTemplateAppService _formTemplateAppService;

    public IndexModel(IFormTemplateAppService formTemplateAppService)
    {
        _formTemplateAppService = formTemplateAppService;
    }

    public virtual async Task OnGetAsync()
    {
        FormItemTypes = (await _formTemplateAppService.GetBaseInfoAsync()).FormItemTypeDefinitions;
    }
}