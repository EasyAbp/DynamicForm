using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.DynamicForm;

public class DynamicFormDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IFormItemProviderResolver _formItemProviderResolver;
    private readonly IFormTemplateRepository _formTemplateRepository;
    private readonly FormTemplateManager _formTemplateManager;

    public DynamicFormDataSeedContributor(
        ICurrentTenant currentTenant,
        IJsonSerializer jsonSerializer,
        IFormItemProviderResolver formItemProviderResolver,
        IFormTemplateRepository formTemplateRepository,
        FormTemplateManager formTemplateManager)
    {
        _currentTenant = currentTenant;
        _jsonSerializer = jsonSerializer;
        _formItemProviderResolver = formItemProviderResolver;
        _formTemplateRepository = formTemplateRepository;
        _formTemplateManager = formTemplateManager;
    }

    [UnitOfWork(true)]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using var change = _currentTenant.Change(context.TenantId);

        await SeedFormTemplatesAsync();
    }

    private async Task SeedFormTemplatesAsync()
    {
        var textBoxFormItemProvider = _formItemProviderResolver.Resolve(TextBoxFormItemProvider.Name);
        var optionButtonsFormItemProvider = _formItemProviderResolver.Resolve(OptionButtonsFormItemProvider.Name);

        var formTemplate1 = await _formTemplateManager.CreateAsync(DynamicFormTestConsts.TestFormDefinitionName,
            DynamicFormTestConsts.FormTemplate1Name, "my-custom-tag");

        formTemplate1.GetType().GetProperty("Id")!.SetValue(formTemplate1, DynamicFormTestConsts.FormTemplate1Id);

        var formItemTemplate1Configurations =
            (TextBoxFormItemConfigurations)await textBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        formItemTemplate1Configurations!.MinLength = 2;
        formItemTemplate1Configurations!.MaxLength = 10;
        formItemTemplate1Configurations!.RegexPattern = "^[A-Za-z\\s]*$"; // letters and spaces only

        await _formTemplateManager.CreateFormItemAsync(
            formTemplate1, "Name", "Your full name.", TextBoxFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate1Configurations), null, 0);

        var formItemTemplate2Configurations =
            (TextBoxFormItemConfigurations)await textBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        await _formTemplateManager.CreateFormItemAsync(
            formTemplate1, "Dept", "Your department name.", TextBoxFormItemProvider.Name, true,
            _jsonSerializer.Serialize(formItemTemplate2Configurations), new AvailableValues { "Dept 1", "Dept 2" }, 1);

        var formItemTemplate3Configurations =
            (OptionButtonsFormItemConfigurations)await optionButtonsFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        await _formTemplateManager.CreateFormItemAsync(
            formTemplate1, "Gender", "Your gender.", OptionButtonsFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate3Configurations), new AvailableValues { "Male", "Female" }, 2);

        var formItemTemplate4Configurations =
            (OptionButtonsFormItemConfigurations)await optionButtonsFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        formItemTemplate4Configurations!.IsMultiSelection = true;
        formItemTemplate4Configurations!.MinSelection = 1;
        formItemTemplate4Configurations!.MaxSelection = 2;

        await _formTemplateManager.CreateFormItemAsync(
            formTemplate1, "Requirements", "Other requirements.", OptionButtonsFormItemProvider.Name, true,
            _jsonSerializer.Serialize(formItemTemplate4Configurations), new AvailableValues
            {
                "Use annual leave", "Urgent", "Remote standby"
            }, 3);

        await _formTemplateRepository.InsertAsync(formTemplate1, true);
    }
}