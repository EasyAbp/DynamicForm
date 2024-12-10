using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms.Dtos;
using JetBrains.Annotations;
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
    Task<FormDto> UpdateFormItemAsync(Guid id, [NotNull] string name, UpdateFormItemDto input);
}