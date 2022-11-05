using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class FormTemplateGetListInput : PagedAndSortedResultRequestDto
{
    public string FormDefinitionName { get; set; }

    public string Name { get; set; }

    public string CustomTag { get; set; }

}