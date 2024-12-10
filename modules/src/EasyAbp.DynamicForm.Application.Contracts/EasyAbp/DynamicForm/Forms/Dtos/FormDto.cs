using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.Forms.Dtos;

[Serializable]
public class FormDto : FullAuditedEntityDto<Guid>
{
    public string FormDefinitionName { get; set; }

    public Guid FormTemplateId { get; set; }

    public string FormTemplateName { get; set; }

    public List<FormItemDto> FormItems { get; set; }
}