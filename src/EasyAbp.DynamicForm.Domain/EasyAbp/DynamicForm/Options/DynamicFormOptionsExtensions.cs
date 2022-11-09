using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;

namespace EasyAbp.DynamicForm.Options;

public static class DynamicFormOptionsExtensions
{
    public static DynamicFormOptions AddTextBoxFormItemType(this DynamicFormOptions options)
    {
        options.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            TextBoxFormItemProvider.Name,
            TextBoxFormItemProvider.LocalizationItemKey,
            typeof(TextBoxFormItemProvider)
        ));

        return options;
    }

    public static DynamicFormOptions AddOptionButtonsFormItemType(this DynamicFormOptions options)
    {
        options.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            OptionButtonsFormItemProvider.Name,
            OptionButtonsFormItemProvider.LocalizationItemKey,
            typeof(OptionButtonsFormItemProvider)
        ));

        return options;
    }
}