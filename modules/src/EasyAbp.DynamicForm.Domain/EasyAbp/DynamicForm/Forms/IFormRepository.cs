using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.DynamicForm.Forms;

public interface IFormRepository : IRepository<Form, Guid>
{
}
