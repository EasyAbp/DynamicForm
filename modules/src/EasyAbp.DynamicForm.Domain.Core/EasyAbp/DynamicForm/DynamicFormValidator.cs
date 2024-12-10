using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm;

public class DynamicFormValidator : IDynamicFormValidator, ITransientDependency
{
    protected IFormItemProviderResolver FormItemProviderResolver { get; }

    public DynamicFormValidator(IFormItemProviderResolver formItemProviderResolver)
    {
        FormItemProviderResolver = formItemProviderResolver;
    }

    public virtual async Task ValidateTemplatesAsync(IEnumerable<IFormItemMetadata> metadataList)
    {
        var listedTemplates = metadataList.ToList();

        if (listedTemplates.Count != listedTemplates.Select(x => x.Name).Distinct().Count())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.DuplicateFormItemTemplate);
        }

        foreach (var template in listedTemplates)
        {
            var provider = FormItemProviderResolver.Resolve(template.Type);

            await provider.ValidateTemplateAsync(template);
        }
    }

    public virtual async Task ValidateValuesAsync(
        IEnumerable<IFormItemMetadata> metadataList, IEnumerable<IFormItem> formItems)
    {
        var listedFormItems = formItems.ToList();

        if (listedFormItems.Count != listedFormItems.Select(x => x.Name).Distinct().Count())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.DuplicateFormItem);
        }

        foreach (var metadata in metadataList.Where(x => !x.Disabled))
        {
            var formItem = listedFormItems.FirstOrDefault(x => x.Name == metadata.Name);

            if (formItem is null)
            {
                throw new BusinessException(DynamicFormCoreErrorCodes.MissingFormItem)
                    .WithData("item", metadata.Name);
            }

            var provider = FormItemProviderResolver.Resolve(metadata.Type);

            await provider.ValidateValueAsync(metadata, formItem.Value);
        }
    }
}