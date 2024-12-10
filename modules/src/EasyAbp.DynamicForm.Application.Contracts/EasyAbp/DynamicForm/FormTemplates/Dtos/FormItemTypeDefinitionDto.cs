namespace EasyAbp.DynamicForm.FormTemplates.Dtos;

public class FormItemTypeDefinitionDto
{
    /// <summary>
    /// Used as Value of `FormItem.Type`.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Localization item key for UI to display the name of this form item type.
    /// </summary>
    public string LocalizationItemKey { get; set; }
    
    /// <summary>
    /// Used as the JSON serialized form item configuration template.
    /// </summary>
    public string ConfigurationsJsonTemplate { get; set; }
}