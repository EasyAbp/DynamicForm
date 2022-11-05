using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form;

public class IndexModel : DynamicFormPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid FormTemplateId { get; set; }

    public FormFilterInput FormFilter { get; set; } = new();

    public virtual async Task OnGetAsync()
    {
        FormFilter.FormTemplateId = FormTemplateId;

        await Task.CompletedTask;
    }
}

public class FormFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormDefinitionName")]
    public string FormDefinitionName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormTemplateId")]
    public Guid? FormTemplateId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormTemplateName")]
    public string FormTemplateName { get; set; }
}