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
        private readonly MemoryCacheService _cacheService;

        public WeatherService(HttpClient httpClient, IOptions<ApiKeys> apiKeys, MemoryCacheService cacheService)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys.Value;
            _cacheService = cacheService;
        }

        public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon, DateTime date)
        {
            string cacheKey = $"weather_{lat}_{lon}_{date:yyyy-MM-dd}";

            return await _cacheService.GetOrSetAsync(cacheKey, async () =>
            {
                string apiKey = _apiKeys.WeatherApiKey;
                string formattedDate = date.ToString("yyyy-MM-dd");

                string url = $"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={lat}&lon={lon}&date={formattedDate}&appid={apiKey}";

                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return new WeatherResponse();
                }

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<WeatherResponse>(content);
            });
        }
    }
}
