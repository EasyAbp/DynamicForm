# DynamicForm

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FDynamicForm%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.DynamicForm.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.DynamicForm.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.DynamicForm.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.DynamicForm.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/DynamicForm?style=social)](https://www.github.com/EasyAbp/DynamicForm)

An ABP module helps users to define and use dynamic forms at runtime.

## Installation

1. Install the following NuGet
   packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.DynamicForm.Application
    * EasyAbp.DynamicForm.Application.Contracts
    * EasyAbp.DynamicForm.Domain
    * EasyAbp.DynamicForm.Domain.Shared
    * EasyAbp.DynamicForm.EntityFrameworkCore
    * EasyAbp.DynamicForm.HttpApi
    * EasyAbp.DynamicForm.HttpApi.Client
    * EasyAbp.DynamicForm.Web

2. Add `DependsOn(typeof(DynamicFormXxxModule))` attribute to configure the module
   dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Add `builder.ConfigureDynamicForm();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

4. Add EF Core migrations and update your database.
   See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

1. Configure the module.

    ```csharp
    Configure<DynamicFormOptions>(options =>
    {
        options.AddOrUpdateFormDefinition(new FormDefinition("InternalForm", "Internal Form"));
    });
    
    Configure<DynamicFormCoreOptions>(options =>
    {
        options.AddTextBoxFormItemType();
        options.AddOptionButtonsFormItemType();
        // Add any type you want, including your custom types....
    });
    ```

2. (Optional) Create a custom `FormTemplateOperationAuthorizationHandler` to determine who can create/read/update/delete
   the form templates. (
   see [sample](https://github.com/EasyAbp/DynamicForm/blob/main/host/EasyAbp.DynamicForm.Web.Unified/InternalFormFormTemplateOperationAuthorizationHandler.cs))
   Users who have `EasyAbp.DynamicForm.FormTemplate.Manage` permission can skip the check.

3. (Optional) Create a custom `FormOperationAuthorizationHandler` to determine who can create/read/update/delete the
   forms. (
   see [sample](https://github.com/EasyAbp/DynamicForm/blob/main/host/EasyAbp.DynamicForm.Web.Unified/InternalFormFormOperationAuthorizationHandler.cs))
   Users who have `EasyAbp.DynamicForm.Form.Manage` permission can skip the check.

4. Try to create a form template.

5. Try to create a form.

![FormTemplates](/docs/images/FormTemplates.png)
![FormItemTemplates](/docs/images/FormItemTemplates.png)
![CreateFormItemTemplate](/docs/images/CreateFormItemTemplate.png)
![Forms](/docs/images/Forms.png)

## Use the Dynamic Form Core Only

This way, we don't install the whole dynamic form module (no extra entities installed). Instead, we enhance the existing
entities to support the dynamic forms feature.

1. Install only the following modules:

    * EasyAbp.DynamicForm.Domain.Core
    * EasyAbp.DynamicForm.Domain.Shared
    * EasyAbp.DynamicForm.EntityFrameworkCore.Shared

2. Make your entities contain the form item information.

    ```csharp
    public class BookRental : AggregateRoot<Guid>
    {
        public string BookName { get; set; }
        public List<BookRentalFormItem> FormItems { get; set; } = new();
    }
    
    public class BookRentalFormItem : Entity, IFormItemTemplate
    {
        // properties...
    } 
    ```

    ```csharp
    public class BookRentalRequest : AggregateRoot<Guid>
    {
        public Guid BookRentalId { get; set; }
        public Guid RenterUserId { get; set; }
        public List<BookRentalRequestFormItem> FormItems { get; set; } = new();
    }
    
    public class BookRentalRequestFormItem : Entity, IFormItem // implement IFormItemMetadata if need
    {
        // properties...
    } 
    ```

    ```csharp
    protected override void OnModelCreating(ModelBuilder builder)
    {
        // ...
        builder.Entity<BookRentalFormItem>(b =>
        {
            b.ToTable(MyProjectConsts.DbTablePrefix + "BookRentalFormItems", MyProjectConsts.DbSchema);
            b.TryConfigureAvailableValues(); // add this configuration.
            b.ConfigureByConvention();
            b.HasKey(x => new { x.BookRentalId, x.Name });
        });
    }
    ```

3. Validate when changing the form item templates.
    ```csharp
    await dynamicFormValidator.ValidateTemplatesAsync(bookRental.FormItems);
    ```

4. Validate when changing the form items.
    ```csharp
    await dynamicFormValidator.ValidateValuesAsync(bookRental.FormItems, bookRentalRequest.FormItems);
    ```

## Roadmap

- [ ] Number input form item type.
- [ ] Checkbox form item type.
- [ ] Date picker form item type.
- [ ] Listbox form item type.
- [ ] Drop-down listbox form item type.