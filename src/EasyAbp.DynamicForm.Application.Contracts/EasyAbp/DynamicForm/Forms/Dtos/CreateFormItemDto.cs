using System;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class CreateFormItemDto
{
    public string Name { get; set; }

    public FormItemType Type { get; set; }

    public string Value { get; set; }
}