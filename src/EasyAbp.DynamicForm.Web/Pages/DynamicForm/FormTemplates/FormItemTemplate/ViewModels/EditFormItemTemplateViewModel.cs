using System.ComponentModel.DataAnnotations;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;

public class EditFormItemTemplateViewModel
{
    [Display(Name = "FormItemTemplateTip")]
    public string InfoText { get; set; }

    [Required]
    [AbpRadioButton(Inline = true)]
    [Display(Name = "FormItemTemplateType")]
    public FormItemType Type { get; set; }

    [Display(Name = "FormItemTemplateOptional")]
    public bool Optional { get; set; }

    /// <summary>
    /// Split available values of radio form item with commas.
    /// </summary>
    [Display(Name = "FormItemTemplateRadioValues")]
    [InputInfoText("FormItemTemplateRadioValuesInfo")]
    public string RadioValues { get; set; }

    [Display(Name = "FormItemDisplayOrder")]
    public int DisplayOrder { get; set; }
}