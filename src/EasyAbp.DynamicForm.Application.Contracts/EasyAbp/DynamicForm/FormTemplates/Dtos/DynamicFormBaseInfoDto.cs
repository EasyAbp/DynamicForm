using System;
using System.Collections.Generic;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class DynamicFormBaseInfoDto
{
    public List<FormItemTypeDefinitionDto> FormItemTypeDefinitions { get; set; }

    public List<FormDefinitionDto> FormDefinitions { get; set; }
}