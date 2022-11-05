using System;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class CreateFormItemDto
{
    public string Name { get; set; } = null!;

    public string Value { get; set; }
}