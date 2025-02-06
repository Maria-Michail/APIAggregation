using AgileActorsApp.Models;

namespace AgileActorsApp.Services.Weather
{
    public interface IWeatherService
    {
        Task<WeatherResponse> GetWeatherAsync(double lat, double lon, DateTime date);
    }
}
