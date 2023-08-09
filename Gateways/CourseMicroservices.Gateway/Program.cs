using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>
{
    string? environmentName = hostingContext.HostingEnvironment.EnvironmentName.ToLower();

    config.AddJsonFile($"configuration.{environmentName}.json").AddEnvironmentVariables();

});






var app = builder.Build();


app.UseOcelot().Wait();



app.Run();
