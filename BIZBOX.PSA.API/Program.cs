using static BIZBOX.PSA.API.Configurations.Services;
using static BIZBOX.PSA.API.Configurations.Endpoints;
using static BIZBOX.PSA.API.Configurations.Swash;
using static BIZBOX.PSA.API.Configurations.Database;
using static BIZBOX.PSA.API.Configurations.Mediator;
using static BIZBOX.PSA.API.Configurations.CORS;
using BIZBOX.PSA.PERSISTENCE.Context;

var builder = WebApplication.CreateBuilder(args);

RegisterEntityFramework(builder);
RegisterSwash(builder);
RegisterMediatr(builder);
RegisterAutoMapper(builder);
RegisterServices(builder);
RegisterEndpoints(builder);
AddFluentValidation(builder);
AddCorsPolicy(builder);

var app = builder.Build();
using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var context = scope.ServiceProvider.GetRequiredService<BPSADbContext>();

ConfigureCors(app);
ConfigureEndpoints(app);
ConfigureSwash(app, builder);
ConfigureDatabaseMigrations(context);
await ConfigureDatabase(services);

app.Run();
