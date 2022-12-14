using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using AutoMapper;

namespace EasyAbp.DynamicForm;

public class DynamicFormApplicationAutoMapperProfile : Profile
{
    public DynamicFormApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        CreateMap<Form, FormDto>();
        CreateMap<FormItem, FormItemDto>();
        CreateMap<FormTemplate, FormTemplateDto>();
        CreateMap<FormItemTemplate, FormItemTemplateDto>();
    }
}
