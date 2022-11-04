using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace EasyAbp.DynamicForm.Pages;

public class IndexModel : DynamicFormPageModel
{
    public void OnGet()
    {

    }

    public async Task OnPostLoginAsync()
    {
        await HttpContext.ChallengeAsync("oidc");
    }
}
