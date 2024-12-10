using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using EasyAbp.DynamicForm;

namespace EasyAbp.DynamicForm.MainDemo.EntityFrameworkCore
{
    [ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
    public interface IMainDemoDbContext : IEfCoreDbContext
    {
    }
}
