namespace EasyAbp.DynamicForm.FormItemTypes;

public interface IFormItemProviderResolver
{
    IFormItemProvider Resolve(string providerName);
}