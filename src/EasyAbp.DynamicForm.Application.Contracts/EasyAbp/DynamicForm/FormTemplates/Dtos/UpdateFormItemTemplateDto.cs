using System;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class UpdateFormItemTemplateDto
{
    public string Tip { get; set; }

    public FormItemType Type { get; set; }

    public bool Optional { get; set; }

    public AvailableRadioValues RadioValues { get; set; }
}