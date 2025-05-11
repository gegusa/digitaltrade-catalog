using DigitalTrade.Catalog.AppServices;
using DigitalTrade.Catalog.Entities;
using DigitalTrade.Catalog.Host.Extensions;
using KafkaFlow;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddEntities(configuration);
builder.Services.AddAppServices();
builder.Services.AddKafkaFlow(configuration);

builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});
builder.Services.AddCors();
builder.Services.AddHttpContextAccessor();
builder.Services.AddHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.Map("/", () => "Hello from Catalog!");
app.MapControllers();
var kafkaBus = app.Services.CreateKafkaBus();
await kafkaBus.StartAsync();

app.Run();