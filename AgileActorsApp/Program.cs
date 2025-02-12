using AgileActorsApp.ApiKey;
using AgileActorsApp.Cache;
using AgileActorsApp.Services;
using AgileActorsApp.Services.Movies;
using AgileActorsApp.Services.News;
using AgileActorsApp.Services.Weather;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();
var configuration = builder.Configuration;
builder.Services.Configure<ApiKeys>(configuration.GetSection("ApiKeys"));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddScoped<IMemoryCacheService, MemoryCacheService>();

builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IMoviesService, MoviesService>();
builder.Services.AddScoped<IAggregationService, AggregationService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
