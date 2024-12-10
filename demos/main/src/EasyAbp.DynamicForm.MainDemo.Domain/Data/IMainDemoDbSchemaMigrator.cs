using System.Threading.Tasks;

namespace EasyAbp.DynamicForm.MainDemo.Data;

public interface IMainDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
