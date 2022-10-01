using Short.Config;
using Short.Extensions;
using Short.Repositories;
using Short.Services;

var configuration = new ConfigurationBuilder()
    .AddJsonFile( "appsettings.json", optional: true, reloadOnChange: true )
    .AddJsonFile( "appsettings.Development.json", optional: true, reloadOnChange: true )
    .AddEnvironmentVariables()
    .Build();

var redisConfig = new RedisConfig( configuration.GetSection( "Redis" ).GetValue<string>("Url") );

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddRedis( redisConfig );

builder.Services.AddSingleton<IShortenerRepository, ShortenerRepository>();
builder.Services.AddSingleton<IShortenerService, ShortenerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
