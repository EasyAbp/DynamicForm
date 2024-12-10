using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.DynamicForm.MainDemo.Pages;

public class IndexModel : DynamicFormMainDemoPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
