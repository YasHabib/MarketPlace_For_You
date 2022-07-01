using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MarketPlaceForYou.Api.SwashBuckle
{/// <summary>
/// Code knows how to use JSON token....something...
/// </summary>
    public class AuthHeaderOperationFilter: IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            // Only authorize the endpoint if it has an Authorize attribute
            if (context.MethodInfo.DeclaringType == null)
                return;
            var isAuthorized = context.MethodInfo.DeclaringType.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any() ||
                               context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>().Any();
            if (!isAuthorized) return;

            // Add the Access Token parameter to the endpoint documentation
            if (operation.Security == null)
                operation.Security = new List<OpenApiSecurityRequirement>();

            var scheme = new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "bearer" } };
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                [scheme] = new List<string>()
            });
        }
    }
}
