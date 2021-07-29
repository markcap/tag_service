using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TagService
{
    public class SwaggerApiGatewayValues : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Servers.Add(new OpenApiServer() { Url = "/tag-service" });
        }
    }
}
