using System;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class UpdateFormItemDto
{
    public string Value { get; set; }
}