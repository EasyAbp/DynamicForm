using System.Threading.Tasks;
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

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(DynamicFormMenus.Prefix, displayName: "DynamicForm", "~/DynamicForm", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}
