using BIZBOX.PSA.API.Configurations.Filters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BIZBOX.PSA.API.Configurations
{
    public static class Endpoints
    {
        internal static void RegisterEndpoints(WebApplicationBuilder builder)
        {
            builder.Configuration["buildVersion"] = "1.0.0";
            builder.Services.AddMvc(options =>
            {
                _ = options.Filters.Add(typeof(CustomExceptionFilterAttribute));
                _ = options.Filters.Add(typeof(ActionValidationFilterAttribute));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            });
        }

        internal static void ConfigureEndpoints(WebApplication app)
        {
            _ = app.UseRouting();
            _ = app.UseAuthorization();
            _ = app.UseAuthentication();
            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
