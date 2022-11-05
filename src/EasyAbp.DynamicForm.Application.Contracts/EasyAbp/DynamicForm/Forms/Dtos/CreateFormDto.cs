using System;
using System.Collections.Generic;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class CreateFormDto
{
    public Guid FormTemplateId { get; set; }

    public List<CreateFormItemDto> FormItems { get; set; }
}