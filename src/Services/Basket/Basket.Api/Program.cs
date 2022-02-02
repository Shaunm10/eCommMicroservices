using Basket.Api.GrpcServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

// Redis configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// General configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.AddScoped<IDiscountService, DiscountService>();
builder.Services.AddAutoMapper(typeof(Program));

// add Grpc Clients
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(o =>
{
    var discountGrpcUrl = builder.Configuration.GetValue<string>("GrpcSettings:DiscountUrl");

    // TODO: Log this Url as information
    o.Address = new Uri(discountGrpcUrl);
});

// Add services for MassTransit/RabbitMQ
builder.Services.AddMassTransit(massTransitConfig =>
{
    massTransitConfig.UsingRabbitMq((context, rabbitMqConfig) =>
    {
        var hostAddress = builder.Configuration.GetValue<string>("EventBusSettings:HostAddress");
        rabbitMqConfig.Host(hostAddress);
    });
});

builder.Services.AddMassTransitHostedService();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
