using CardsApiWithControllers.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<IDbService, DbService>();
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseAuthorization();

app.MapControllers();

app.Run();
