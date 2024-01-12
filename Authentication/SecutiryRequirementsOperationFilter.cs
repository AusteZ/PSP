using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PSP.Authentication;
public class SecurityRequirementsOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var attributes = context.MethodInfo.GetCustomAttributes(true).Union(context.MethodInfo.DeclaringType.GetCustomAttributes(true));

        var authorizeAttribute = attributes.OfType<AuthorizeAttribute>().FirstOrDefault();
        if (authorizeAttribute != null)
        {
            var securityRequirement = new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            };
            operation.Security = new List<OpenApiSecurityRequirement> { securityRequirement };
        }
    }
}