using System.Threading.Tasks;
using EasyAbp.DynamicForm.Localization;
using EasyAbp.DynamicForm.Permissions;
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
        context.Menu.AddItem(new ApplicationMenuItem(DynamicFormMenus.Prefix, displayName: "DynamicForm",
            "~/DynamicForm", icon: "fa fa-wpforms"));

        if (await context.IsGrantedAsync(DynamicFormPermissions.Form.Default))
        {
            context.Menu.AddItem(
                new ApplicationMenuItem(DynamicFormMenus.Form, l["Menu:Form"], "/DynamicForm/Forms/Form")
            );
        }
    }
}