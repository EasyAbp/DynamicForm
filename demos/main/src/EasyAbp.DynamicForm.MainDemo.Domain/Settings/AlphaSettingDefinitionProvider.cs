using EasyAbp.DynamicForm.MainDemo.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.DynamicForm.MainDemo.Settings;

public class AlphaSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AlphaSettings.MySetting1));

        //Gridin son filtre ayarlarını anımsa
        context.Add(new SettingDefinition(AlphaSettings.RememberGridFilterState, "false", L("DisplayName:EasyAbp.DynamicForm.RememberGridFilterState"), L("Description:EasyAbp.DynamicForm.RememberGridFilterState")));
    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MainDemoResource>(name);
    }
}
