using AutoMapper;
using BIZBOX.PSA.APPLICATION.RequestResponse;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;

namespace BIZBOX.PSA.API.Configurations
{
    public class Mediator
    {
        internal static void RegisterMediatr(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(typeof(RecordNotFoundException).Assembly);
        }

        //https://docs.fluentvalidation.net/en/latest/built-in-validators.html
        internal static void AddFluentValidation(WebApplicationBuilder builder)
        {
            builder.Services.AddFluentValidationAutoValidation();
            //builder.Services.AddFluentValidationClientsideAdapters();
            builder.Services.AddValidatorsFromAssemblyContaining<RecordNotFoundException>();
        }


        internal static void RegisterAutoMapper(WebApplicationBuilder builder)
        {
            builder.Services.AddAutoMapper(
                configAction =>
                {
                    configAction.ValidateInlineMaps = false;
                },
                typeof(Response)
            );
        }
    }
}
