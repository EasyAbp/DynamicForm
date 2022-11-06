using System;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class CreateFormItemTemplateDto
{
    public string Name { get; set; }

    public string InfoText { get; set; }

    public FormItemType Type { get; set; }

    public bool Optional { get; set; }

    public AvailableRadioValues RadioValues { get; set; }

    public int DisplayOrder { get; set; }
}