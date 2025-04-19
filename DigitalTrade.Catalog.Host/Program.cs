using DigitalTrade.Catalog.AppServices;
using DigitalTrade.Catalog.Entities;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Services.AddMvc();
builder.Services.AddControllers();

builder.Services.AddEntities(configuration);
builder.Services.AddAppServices();

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

app.MapControllers();
app.Run();
