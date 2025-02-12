using AgileActorsApp.ApiKey;
using AgileActorsApp.Cache;
using AgileActorsApp.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AgileActorsApp.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeys _apiKeys;
        private readonly IMemoryCacheService _cacheService;
        private readonly ILogger<WeatherService> _logger;

        public WeatherService(HttpClient httpClient, IOptions<ApiKeys> apiKeys, IMemoryCacheService cacheService, ILogger<WeatherService> logger)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon, DateTime date)
        {
            string cacheKey = $"weather_{lat}_{lon}_{date:yyyy-MM-dd}";

            var weatherResponse = await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                string apiKey = _apiKeys.WeatherApiKey;
                string formattedDate = date.ToString("yyyy-MM-dd");

                string url = $"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={lat}&lon={lon}&date={formattedDate}&appid={apiKey}";

                try
                {
                    var response = await _httpClient.GetAsync(url);

                    if (!response.IsSuccessStatusCode)
                    {
                        _logger.LogError("Failed to fetch weather data from API. StatusCode: {StatusCode}, URL: {Url}", response.StatusCode, url);
                        return new WeatherResponse();
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var weatherResponse = JsonSerializer.Deserialize<WeatherResponse>(content);

                    _logger.LogInformation("Successfully fetched weather data for {Lat}, {Lon} on {Date}.", lat, lon, date.ToString("yyyy-MM-dd"));

                    return weatherResponse;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while fetching weather data for coordinates: {Lat}, {Lon} on {Date}", lat, lon, date.ToString("yyyy-MM-dd"));
                    return new WeatherResponse();
                }
            });
            return weatherResponse ?? new WeatherResponse();
        }
    }
}
