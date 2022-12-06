using System.Threading.Tasks;
using EasyAbp.DynamicForm.Localization;
using Volo.Abp.UI.Navigation;

namespace EasyAbp.DynamicForm.Web.Menus;

public class DynamicFormMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<DynamicFormResource>();
        //Add main menu items.

        var dynamicFormMenu = new ApplicationMenuItem(DynamicFormMenus.Prefix, displayName: l["Menu:DynamicForm"],
            "~/DynamicForm/FormTemplates/FormTemplate", icon: "fa fa-wpforms");

        context.Menu.AddItem(dynamicFormMenu);
    }
}