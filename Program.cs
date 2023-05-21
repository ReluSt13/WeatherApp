using Microsoft.EntityFrameworkCore;
using WeatherApp.Data.Repositories;
using WeatherApp.EntityFramework;
using WeatherApp.Services;
using WeatherApp.Services.Weather;
using WeatherApp.Services.Location;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddTransient<ICacheService, CacheService>();
builder.Services.AddTransient<IWeatherRepository, WeatherRepository>();
builder.Services.AddTransient<IWeatherService, WeatherService>();
builder.Services.AddTransient<IWeatherCacheService, WeatherService>();
builder.Services.Decorate<IWeatherCacheService, WeatherCacheService>();
builder.Services.AddTransient<ILocationService, LocationService>();
builder.Services.AddTransient<ILocationCacheService, LocationService>();
builder.Services.Decorate<ILocationCacheService, LocationCacheService>();
builder.Services.AddTransient<IReverseLocationService, ReverseLocationService>();
builder.Services.AddTransient<IReverseLocationCacheService, ReverseLocationService>();
builder.Services.Decorate<IReverseLocationCacheService, ReverseLocationCacheService>();

string connectionString = builder.Configuration["ConnectionStrings:Default"];
builder.Services.AddDbContext<WeatherContext>(options =>
{
    options
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(cors =>
{
    cors
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
