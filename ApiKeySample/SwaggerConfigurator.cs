using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

internal static class SwaggerConfigurator
{
    internal static void Configure(SwaggerGenOptions x)
    {
        OpenApiSecurityScheme queryScheme = new OpenApiSecurityScheme
        {
            Name = "api_key",
            Description = "Api Key Authentication",
            In = ParameterLocation.Query,
            Type = SecuritySchemeType.ApiKey,
            Reference = new OpenApiReference { Id = "Query", Type = ReferenceType.SecurityScheme }
        };

        x.AddSecurityDefinition(queryScheme.Reference.Id, queryScheme);
        x.AddSecurityRequirement(new OpenApiSecurityRequirement { { queryScheme, Array.Empty<string>() }, });
    }
}