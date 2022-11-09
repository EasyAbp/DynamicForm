using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface ICustomFormItemValidator
{
    Task ValidateValueAsync(IFormItemMetadata formItemMetadata, [CanBeNull] string value);
}