using System;
using EasyAbp.DynamicForm.Forms.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.Forms;

public interface IFormAppService :
    ICrudAppService<
        FormDto,
        Guid,
        FormGetListInput,
        CreateFormDto,
        UpdateFormDto>
{
}