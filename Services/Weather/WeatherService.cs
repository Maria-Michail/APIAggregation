using AgileActorsApp.ApiKey;
using AgileActorsApp.Models;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace AgileActorsApp.Services.Weather
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiKeys _apiKeys;

        public WeatherService(HttpClient httpClient, IOptions<ApiKeys> apiKeys)
        {
            _httpClient = httpClient;
            _apiKeys = apiKeys.Value;
        }

        public async Task<WeatherResponse> GetWeatherAsync(double lat, double lon, DateTime date)
        {
            string apiKey = _apiKeys.WeatherApiKey;
            string formattedDate = date.ToString("yyyy-MM-dd");

            string url = $"https://api.openweathermap.org/data/3.0/onecall/day_summary?lat={lat}&lon={lon}&date={formattedDate}&appid={apiKey}";

            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WeatherResponse>(content);
        }
    }
}
