using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.DynamicForm.MongoDB;

[ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
public interface IDynamicFormMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
