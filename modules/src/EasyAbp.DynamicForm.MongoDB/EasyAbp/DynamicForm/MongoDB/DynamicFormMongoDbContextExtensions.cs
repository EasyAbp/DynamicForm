using Volo.Abp;
using Volo.Abp.MongoDB;

namespace EasyAbp.DynamicForm.MongoDB;

public static class DynamicFormMongoDbContextExtensions
{
    public static void ConfigureDynamicForm(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
