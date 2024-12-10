using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace EasyAbp.DynamicForm.MainDemo
{
    public abstract class DemoDomainService : DomainService
    {
        protected ICurrentUser CurrentUser => LazyServiceProvider.LazyGetRequiredService<ICurrentUser>();
    }
}