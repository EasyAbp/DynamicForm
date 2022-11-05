using System;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class FormItemTemplateDto : EntityDto
{
    public Guid FormTemplateId { get; set; }

    public string Name { get; set; }

    public string Tip { get; set; }

    public FormItemType Type { get; set; }

    public bool Optional { get; set; }

    public AvailableRadioValues RadioValues { get; set; }
}