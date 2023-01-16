using System;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class FormItemTemplateDto : EntityDto
{
    public Guid FormTemplateId { get; set; }

    public string Name { get; set; }

    public string Group { get; set; }

    public string InfoText { get; set; }

    public string Type { get; set; }

    public bool Optional { get; set; }

    public string Configurations { get; set; }

    public AvailableValues AvailableValues { get; set; }

    public int DisplayOrder { get; set; }

    public bool Disabled { get; set; }
}