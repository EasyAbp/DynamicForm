using AutoMapper;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;

namespace EasyAbp.DynamicForm.Web;

public class DynamicFormWebAutoMapperProfile : Profile
{
    public DynamicFormWebAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<FormTemplateDto, EditFormTemplateViewModel>();
        CreateMap<EditFormTemplateViewModel, UpdateFormTemplateDto>();
    }
}