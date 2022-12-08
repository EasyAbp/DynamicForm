using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.FileBox;

public class UrlsFileBoxValueValidator : IFileBoxValueValidator, ITransientDependency
{
    public static List<string> ValidUriSchemes = new List<string>()
    {
        Uri.UriSchemeHttp,
        Uri.UriSchemeHttps
    };

    protected IJsonSerializer JsonSerializer { get; }

    public UrlsFileBoxValueValidator(IJsonSerializer jsonSerializer)
    {
        JsonSerializer = jsonSerializer;
    }

    public virtual Task ValidateAsync(IFormItemMetadata metadata, string value)
    {
        // You can override this method if the value is not a URL collection.

        if (value.IsNullOrEmpty())
        {
            value = "[]";
        }

        List<string> urls;

        try
        {
            urls = JsonSerializer.Deserialize<List<string>>(value);
        }
        catch
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        if (!metadata.Optional && urls.IsNullOrEmpty())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired);
        }

        foreach (var url in urls)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) || !ValidUriSchemes.Contains(uri.Scheme))
            {
                throw new BusinessException(DynamicFormCoreErrorCodes.FileBoxInvalidUrls);
            }
        }

        return Task.CompletedTask;
    }
}