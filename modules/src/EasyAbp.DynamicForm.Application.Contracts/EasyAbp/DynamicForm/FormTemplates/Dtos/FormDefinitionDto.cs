using System;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class FormDefinitionDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }
}