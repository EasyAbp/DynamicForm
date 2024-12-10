using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace EasyAbp.DynamicForm.MongoDB;

[ConnectionStringName(DynamicFormDbProperties.ConnectionStringName)]
public class DynamicFormMongoDbContext : AbpMongoDbContext, IDynamicFormMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureDynamicForm();
    }
}
