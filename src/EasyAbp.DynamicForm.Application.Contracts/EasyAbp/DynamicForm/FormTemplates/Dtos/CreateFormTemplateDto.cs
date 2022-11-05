using System;
using System.Collections.Generic;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class CreateFormTemplateDto
{
    public string FormDefinitionName { get; set; }

    public string Name { get; set; }

    public string CustomTag { get; set; }

    public List<CreateFormItemTemplateDto> FormItemTemplates { get; set; }
}