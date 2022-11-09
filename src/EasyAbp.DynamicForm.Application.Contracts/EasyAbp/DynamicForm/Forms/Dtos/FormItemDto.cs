using System;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class FormItemDto : EntityDto
{
    public Guid FormId { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public bool Optional { get; set; }

    public string Configurations { get; set; }

    public AvailableValues AvailableValues { get; set; }

    public string Value { get; set; }
}