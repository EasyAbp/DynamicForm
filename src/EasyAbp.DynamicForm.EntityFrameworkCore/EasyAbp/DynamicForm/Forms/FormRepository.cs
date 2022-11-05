using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.Forms;

public class FormRepository : EfCoreRepository<IDynamicFormDbContext, Form, Guid>, IFormRepository
{
    public FormRepository(IDbContextProvider<IDynamicFormDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<Form>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}