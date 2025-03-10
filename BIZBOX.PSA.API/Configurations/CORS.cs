﻿using Microsoft.AspNetCore.HttpOverrides;

namespace BIZBOX.PSA.API.Configurations
{
    public static class CORS
    {
        internal static void AddCorsPolicy(WebApplicationBuilder builder)
        {
            var corsOptions = new CorsOptions();
            builder.Configuration.GetSection("Cors").Bind(corsOptions);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("default", policy => policy
                    .WithOrigins(corsOptions.AllowedOrigins.ToArray())
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    );
            });

            builder.Services.AddControllers();
        }

        internal static void ConfigureCors(WebApplication app)
        {
            app.UseCors("default");
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
        }


        public class CorsOptions
        {
            public List<string> AllowedOrigins { get; set; } = [];
        }
    }
}
