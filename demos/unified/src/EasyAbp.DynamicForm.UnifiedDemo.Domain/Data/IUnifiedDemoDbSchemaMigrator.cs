using System.Threading.Tasks;

namespace EasyAbp.DynamicForm.UnifiedDemo.Data;

public interface IUnifiedDemoDbSchemaMigrator
{
    Task MigrateAsync();
}
