using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormItemTemplate.ViewModels;

public class CreateFormItemTemplateViewModel
{
    [Display(Name = "FormItemTemplateName")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "FormItemTemplateGroup")]
    public string Group { get; set; }

    [Display(Name = "FormItemTemplateInfoText")]
    public string InfoText { get; set; }

    [Display(Name = "FormItemTemplateType")]
    [SelectItems("FormItemTypes")]
    [Required]
    [DisabledInput]
    public string Type { get; set; }

    [Display(Name = "FormItemTemplateOptional")]
    public bool Optional { get; set; }

    [Display(Name = "FormItemTemplateConfigurations")]
    [TextArea(Rows = 5)]
    public string Configurations { get; set; }

    /// <summary>
    /// Split available values with commas.
    /// </summary>
    [Display(Name = "FormItemTemplateAvailableValues")]
    [InputInfoText("FormItemTemplateAvailableValuesInfo")]
    public string AvailableValues { get; set; }

    [Display(Name = "FormItemTemplateDisplayOrder")]
    public int DisplayOrder { get; set; }

    [Display(Name = "FormItemTemplateDisabled")]
    public bool Disabled { get; set; }
}