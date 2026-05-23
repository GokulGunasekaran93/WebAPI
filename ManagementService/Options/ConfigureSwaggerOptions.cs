using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ManagementService.Options;

public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }
    
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            var info = new OpenApiInfo()
            {
                Title = $"Sample API " +  description.GroupName,
                Version = description.ApiVersion.ToString(),
                Description = "API Versioningg" + description.GroupName.ToUpperInvariant(),
            };
            options.SwaggerDoc(description.GroupName, info);
            // to control which api will be in swagger
            options.DocInclusionPredicate((docName, apiDesc) => apiDesc.GroupName == docName);

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }
        }
    }
}