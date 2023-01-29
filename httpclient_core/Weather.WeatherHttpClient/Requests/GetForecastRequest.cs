using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Weather.WeatherHttpClient.Requests

{
    public class GetForecastRequest: HttpRequestMessage
    {
        public GetForecastRequest(string city, DateTime date):
            base(HttpMethod.Get, 
                new Uri($"weather/forecast/{city}/{HttpUtility.UrlEncode(date.ToUniversalTime().ToString("o"))}"
                , UriKind.Relative))
        {

        }
    }
}
