using EasyAbp.DynamicForm.UnifiedDemo.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace EasyAbp.DynamicForm.UnifiedDemo.Settings;

public class UnifiedDemoSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AlphaSettings.MySetting1));

        //Gridin son filtre ayarlarını anımsa
        context.Add(new SettingDefinition(UnifiedDemoSettings.RememberGridFilterState, "false", L("DisplayName:EasyAbp.DynamicForm.RememberGridFilterState"), L("Description:EasyAbp.DynamicForm.RememberGridFilterState")));
    }
    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<UnifiedDemoResource>(name);
    }
}
