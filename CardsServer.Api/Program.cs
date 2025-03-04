using MediatR;
using CommonFiles.Services;
using CardsServer.Data;
using System.Reflection;
using CardsServer.Api.Behaviors;
using CardsServer.Api.FileLogger;
//using Microsoft.Extensions.Logging.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = Assembly.Load("CardsServer.Application");
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
builder.Services.AddSingleton<IConfigDataService, ConfigDataService>();
builder.Logging.AddProvider(new FileLoggerProvider("log.txt"));
builder.Services.AddSingleton<CardsRepository>();
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthorization();

app.MapControllers();

app.Run();
