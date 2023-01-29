using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.WeatherHttpClient.Configurations
{
    public interface IWeatherHttpSettings
    {
        string BaseUri { get; }

        string AccessToken { get; }
    }
}
