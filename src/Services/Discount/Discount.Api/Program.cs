using Discount.Business.Extensions;
using Discount.Business.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IDiscountRepository, DiscountRepository>();

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

var npgConnectionString = app.Services.GetService<IConfiguration>()?.GetValue<string>("DatabaseSettings:ConnectionString");

if (npgConnectionString != null)
{
    // TODO: add more complete DB Migration process.
    app.Services.MigrateDatabase<Program>(npgConnectionString);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();

app.Run();
