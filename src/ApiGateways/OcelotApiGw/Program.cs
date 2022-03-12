using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);

// the new way to do this in .net 6
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var environmentName = builder.Environment.EnvironmentName;

// pull the config for the current environment.
builder.Configuration.AddJsonFile($"ocelot.{environmentName}.json", true, true);

// adds cacheing to Ocelot
builder.Services.AddOcelot()
    .AddCacheManager(settings => settings.WithDictionaryHandle());

var app = builder.Build();
app.MapGet("/", () => "Ocelot Gateway.");

app.UseOcelot();

app.Run();
