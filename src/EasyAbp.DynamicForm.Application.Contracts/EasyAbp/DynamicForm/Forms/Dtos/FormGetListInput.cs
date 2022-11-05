using System;
using System.ComponentModel;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class FormGetListInput : PagedAndSortedResultRequestDto
{
    public string FormDefinitionName { get; set; }

    public Guid? FormTemplateId { get; set; }

    public string FormTemplateName { get; set; }

}