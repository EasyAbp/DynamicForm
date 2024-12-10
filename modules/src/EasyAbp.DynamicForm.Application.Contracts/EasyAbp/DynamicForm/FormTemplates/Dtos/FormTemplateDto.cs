using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

[Serializable]
public class FormTemplateDto : FullAuditedEntityDto<Guid>
{
    public string FormDefinitionName { get; set; }

    public string Name { get; set; }

    public string CustomTag { get; set; }

    public List<FormItemTemplateDto> FormItemTemplates { get; set; }
}