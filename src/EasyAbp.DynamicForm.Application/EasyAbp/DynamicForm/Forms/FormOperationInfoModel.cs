using System;

namespace EasyAbp.DynamicForm.Forms;

public class FormOperationInfoModel
{
    public string FormDefinitionName { get; set; }

    public Guid? FormTemplateId { get; set; }

    public Guid? CreatorId { get; set; }

    public Form Form { get; set; }
}