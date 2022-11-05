using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate;

public class IndexModel : DynamicFormPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}
