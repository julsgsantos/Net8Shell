using BIZBOX.PSA.SERVICES.GuidGenerator.V2_QMS_Integration.Services.GuidGenerator;

namespace BIZBOX.PSA.API.Configurations
{
    public class Services
    {
        internal static void RegisterServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IGuidGeneratorService, GuidGeneratorService>();
        }
    }
}
