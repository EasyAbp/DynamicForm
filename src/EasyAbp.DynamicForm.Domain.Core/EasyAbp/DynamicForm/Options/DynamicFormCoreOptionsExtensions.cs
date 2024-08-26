using EasyAbp.DynamicForm.FormItemTypes.ColorPicker;
using EasyAbp.DynamicForm.FormItemTypes.FileBox;
using EasyAbp.DynamicForm.FormItemTypes.NumberBox;
using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;
using EasyAbp.DynamicForm.FormItemTypes.TimePicker;
using EasyAbp.DynamicForm.FormItemTypes.Toggle;

namespace EasyAbp.DynamicForm.Options;

public static class DynamicFormCoreOptionsExtensions
{
    public static DynamicFormCoreOptions AddBuiltInFormItemTypes(this DynamicFormCoreOptions coreOptions)
    {
        AddTextBoxFormItemType(coreOptions);
        AddOptionButtonsFormItemType(coreOptions);
        AddFileBoxFormItemType(coreOptions);
        AddNumberBoxFormItemType(coreOptions);
        AddToggleFormItemType(coreOptions);
        AddTimePickerFormItemType(coreOptions);
        AddColorPickerFormItemType(coreOptions);

        return coreOptions;
    }

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

    public static DynamicFormCoreOptions AddFileBoxFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            FileBoxFormItemProvider.Name,
            FileBoxFormItemProvider.LocalizationItemKey,
            typeof(FileBoxFormItemProvider)
        ));

        return coreOptions;
    }

    public static DynamicFormCoreOptions AddNumberBoxFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            NumberBoxFormItemProvider.Name,
            NumberBoxFormItemProvider.LocalizationItemKey,
            typeof(NumberBoxFormItemProvider)
        ));

        return coreOptions;
    }

    public static DynamicFormCoreOptions AddToggleFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            ToggleFormItemProvider.Name,
            ToggleFormItemProvider.LocalizationItemKey,
            typeof(ToggleFormItemProvider)
        ));

        return coreOptions;
    }

    public static DynamicFormCoreOptions AddTimePickerFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            TimePickerFormItemProvider.Name,
            TimePickerFormItemProvider.LocalizationItemKey,
            typeof(TimePickerFormItemProvider)
        ));

        return coreOptions;
    }

    public static DynamicFormCoreOptions AddColorPickerFormItemType(this DynamicFormCoreOptions coreOptions)
    {
        coreOptions.AddOrUpdateFormItemTypeDefinition(new FormItemTypeDefinition(
            ColorPickerFormItemProvider.Name,
            ColorPickerFormItemProvider.LocalizationItemKey,
            typeof(ColorPickerFormItemProvider)
        ));

        return coreOptions;
    }
}