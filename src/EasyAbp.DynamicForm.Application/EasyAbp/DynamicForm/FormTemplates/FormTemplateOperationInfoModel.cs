namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateOperationInfoModel
{
    public string FormDefinitionName { get; set; }

    public string Name { get; set; }

    public string CustomTag { get; set; }

    public FormTemplate FormTemplate { get; set; }
}