using System;
using System.Collections.Generic;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class UpdateFormDto
{
    public List<UpdateFormItemDto> FormItems { get; set; }
}