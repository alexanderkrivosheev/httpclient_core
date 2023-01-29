using Microsoft.Extensions.Logging;
using Weather.Models;
using Weather.WeatherHttpClient.Requests;

namespace Weather.WeatherHttpClient
{
    public class WeatherHttpClient : HttpClientBase, IWeatherHttpClient
    {
        public WeatherHttpClient(HttpClient client, ILogger log):base(client,log)
        {
            
        }
    
        public async Task<bool> UpdateWeatherMetricsAsync(string city, DateTime date, int temperature)
        {
            UpdateWeatherMetricsRequest request = new UpdateWeatherMetricsRequest(city, date, temperature);
            HttpClientResponce<string> result = await SendAsync<string>(request);

            return result.IsSuccess;
        }

        public async Task<WeatherForecast> GetForecastAsync(string city, DateTime date)
        {
            GetForecastRequest request = new GetForecastRequest(city, date);
            HttpClientResponce<WeatherForecast> result = await SendAsync<WeatherForecast>(request);
            return result.Content;
        }
    }
}
