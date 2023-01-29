using Weather.Models;

namespace Weather.WeatherHttpClient
{
    public interface IWeatherHttpClient
    {
        Task<WeatherForecast> GetForecastAsync(string city, DateTime date);
        Task<bool> UpdateWeatherMetricsAsync(string city, DateTime date, int temperature);
    }
}
