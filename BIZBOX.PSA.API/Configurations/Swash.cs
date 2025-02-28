using Microsoft.OpenApi.Models;

namespace BIZBOX.PSA.API.Configurations
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    public static class Swash
    {

        internal static void RegisterSwash(WebApplicationBuilder builder)
        {
            var aspEnv = builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value;
            var clientEnv = builder.Configuration.GetSection("Client_Environment")?.Value;

            if (clientEnv == "Local" || aspEnv == "Development" || aspEnv == "Production" || aspEnv == "Test")
            {
                builder.Services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = $"Bizbox-PSA.API {aspEnv}",
                        Description = $"RESTFul Api for Bizbox-PSA.API Version: {builder.Configuration["buildVersion"]}"
                    });
                    options.AddSecurityDefinition("JWT", new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.ApiKey,
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                    });
                    options.CustomSchemaIds(i => i.FullName);
                });
            }
        }

        internal static void ConfigureSwash(WebApplication app, WebApplicationBuilder builder)
        {
            var aspEnv = builder.Configuration.GetSection("ASPNETCORE_ENVIRONMENT")?.Value;

            if (app.Environment.IsDevelopment() || app.Environment.IsProduction() || aspEnv == "Local" || aspEnv == "Test")
            {
                app.UseSwagger(options =>
                {
                    options.SerializeAsV2 = true;
                });

                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                });
            }
        }
    }
}
