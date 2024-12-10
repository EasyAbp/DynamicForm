using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateRepository : EfCoreRepository<IDynamicFormDbContext, FormTemplate, Guid>, IFormTemplateRepository
{
    public FormTemplateRepository(IDbContextProvider<IDynamicFormDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override async Task<IQueryable<FormTemplate>> WithDetailsAsync()
    {
        return (await GetQueryableAsync()).IncludeDetails();
    }
}