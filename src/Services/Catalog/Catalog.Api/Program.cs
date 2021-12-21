using Catalog.Api.Data;
using Catalog.Api.Middleware;
using Catalog.Api.Repositories;
using Serilog;
using Serilog.Events;

var assemblyName = typeof(Program).Assembly.GetName().Name;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("Assembly", assemblyName)
    .CreateBootstrapLogger();

try
{
    Log.Information("Starting Catalog Api");
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console());

    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.AddScoped<ICatalogContext, CatalogContext>();
    builder.Services.AddScoped<IProductRepository, ProductRepository>();

    var app = builder.Build();

    app.UseMiddleware<CustomExceptionHandlingMiddleware>();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}