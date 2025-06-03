using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace WebAPI.Swagger;

public class SwaggerUIOptionsConfiguration : IConfigureOptions<SwaggerUIOptions>
{
    public void Configure(SwaggerUIOptions options)
    {
        options.DisplayRequestDuration();
        options.EnableTryItOutByDefault();
    }
}
