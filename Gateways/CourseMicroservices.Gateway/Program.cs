using CourseMicroservices.Shared.Extantion;
using CourseMicroservices.Shared.Settings;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddCustomTokenAuth(AudiencesName.GatewayMicroservice,TokenVerifyScheme.GatewaySchema);


builder.Services.AddOcelot();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    string? environmentName = hostingContext.HostingEnvironment.EnvironmentName.ToLower();

    config.AddJsonFile($"configuration.{environmentName}.json").AddEnvironmentVariables();

});



var app = builder.Build();

app.UseAuthorization();
app.UseAuthentication();
app.UseOcelot().Wait();

app.Run();
