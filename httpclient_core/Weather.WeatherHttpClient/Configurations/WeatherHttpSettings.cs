using Microsoft.Extensions.Options;

namespace Weather.WeatherHttpClient.Configurations
{
    public class WeatherHttpSettings : IWeatherHttpSettings
    {
        public WeatherHttpSettings(IOptions<WeatherOptions> options)
        {
            BaseUri = options?.Value?.BaseUrl;
            AccessToken = options?.Value?.AccessToken;
        }

        public string BaseUri { get; }
        public string AccessToken { get; }
    }
}
