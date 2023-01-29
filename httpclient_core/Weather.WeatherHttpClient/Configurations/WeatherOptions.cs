using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.WeatherHttpClient.Configurations
{
    public class WeatherOptions
    {
        public const string WeatherConfiguration = "WeatherService";
        public string BaseUrl { get; set; } = string.Empty;
        public string AccessToken { get; set; } = string.Empty;
    }
}
