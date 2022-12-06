using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate;

public class IndexModel : DynamicFormPageModel
{
    [BindProperty(SupportsGet = true)]
    public string FormDefinitionName { get; set; }

    [BindProperty(SupportsGet = true)]
    public string CustomTag { get; set; }

    public FormTemplateFilterInput FormTemplateFilter { get; set; } = new();

    public virtual async Task OnGetAsync()
    {
        FormTemplateFilter.FormDefinitionName = FormDefinitionName;
        FormTemplateFilter.CustomTag = CustomTag;

        await Task.CompletedTask;
    }
}

public class FormTemplateFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateFormDefinitionName")]
    public string FormDefinitionName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateName")]
    public string Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateCustomTag")]
    public string CustomTag { get; set; }
}