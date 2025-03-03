using MediatR;
using CommonFiles.Models;
using CommonFiles.Services;
using Microsoft.Extensions.DependencyInjection;
using CardsServer.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = Assembly.Load("CardsServer.Application");
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddSingleton<IConfigDataService, ConfigDataService>();
builder.Services.AddSingleton<CardsRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthorization();

app.MapControllers();

app.Run();
