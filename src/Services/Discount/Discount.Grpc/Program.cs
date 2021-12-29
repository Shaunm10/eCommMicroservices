using Discount.Common.Extensions;
using Discount.Common.Repositories;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();

builder.Services.AddScoped<IDiscountRepository>(x =>
{
    var npgConnectionString = app.Services.GetService<IConfiguration>()?.GetValue<string>("DatabaseSettings:ConnectionString");

    return new DiscountRepository(npgConnectionString!);
});

// builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();


// Configure the HTTP request pipeline.

app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

var npgConnectionString = app.Services.GetService<IConfiguration>()?.GetValue<string>("DatabaseSettings:ConnectionString");

// TODO: add more complete DB Migration process.
app.Services.MigrateDatabase<Program>(npgConnectionString);

app.Run();
