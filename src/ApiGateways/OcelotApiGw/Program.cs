using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// the new way to do this in .net 6
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var environmentName = builder.Environment.EnvironmentName;

// pull the config for the current environment.
builder.Configuration.AddJsonFile($"ocelot.{environmentName}.json",true, true);

builder.Services.AddOcelot();

var app = builder.Build();

await app.UseOcelot();

// app.MapGet("/", () => "Hello World!");

app.Run();
