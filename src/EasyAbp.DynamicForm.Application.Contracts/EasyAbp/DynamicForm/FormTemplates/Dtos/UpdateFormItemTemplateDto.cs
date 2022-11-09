using System;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class UpdateFormItemTemplateDto
{
    public string InfoText { get; set; }

    public string Type { get; set; }

    public bool Optional { get; set; }

    public string Configurations { get; set; }

    public AvailableValues AvailableValues { get; set; }

    public int DisplayOrder { get; set; }
}