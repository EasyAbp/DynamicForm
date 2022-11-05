using System;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class FormItemDto : EntityDto
{
    public Guid FormId { get; set; }

    public string Name { get; set; }

    public FormItemType Type { get; set; }

    public string Value { get; set; }
}