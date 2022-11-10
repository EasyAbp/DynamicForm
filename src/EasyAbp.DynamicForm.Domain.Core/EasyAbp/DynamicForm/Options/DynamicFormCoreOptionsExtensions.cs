using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;

namespace EasyAbp.DynamicForm.Options;

public static class DynamicFormCoreOptionsExtensions
{
    public static DynamicFormCoreOptions AddTextBoxFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            TextBoxFormItemProvider.Name,
            TextBoxFormItemProvider.LocalizationItemKey,
            typeof(TextBoxFormItemProvider)
        ));

        return coreOptions;
    }

    public static DynamicFormCoreOptions AddOptionButtonsFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            OptionButtonsFormItemProvider.Name,
            OptionButtonsFormItemProvider.LocalizationItemKey,
            typeof(OptionButtonsFormItemProvider)
        ));

        return coreOptions;
    }
}