using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;

namespace Labs.NET.Oracle.WebAPI.ConfigureOptions
{
    public class ConfigureApiExplorerOptions : IConfigureOptions<ApiExplorerOptions>
    {
        public void Configure(ApiExplorerOptions options)
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        }
    }
}
