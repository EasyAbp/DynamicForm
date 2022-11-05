using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormTemplateRepository : IRepository<FormTemplate, Guid>
{
}
