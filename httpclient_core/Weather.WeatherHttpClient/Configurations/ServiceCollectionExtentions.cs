using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Weather.WeatherHttpClient.Configurations;
using Weather.WeatherHttpClient.Handlers;

namespace Weather.WeatherHttpClient
{
    public static class ServiceCollectionExtentions
    {
        public static void AddWeatherHttpClient(this IServiceCollection services)
        {
            services.AddOptions<WeatherOptions>().Configure<IConfiguration>((settings, configuration) =>
            {
                configuration.GetSection(WeatherOptions.WeatherConfiguration).Bind(settings);
            }); 

            services.AddSingleton<IWeatherHttpSettings, WeatherHttpSettings>();
            services.AddSingleton<IWeatherHttpClient, WeatherHttpClient>();
            services.AddSingleton<AuthRequestHandler>();
            
            services.AddHttpClient<IWeatherHttpClient, WeatherHttpClient>((serviceProvider, client) =>
            {
                var clientConfig = serviceProvider.GetRequiredService<IWeatherHttpSettings>();
            
            }).AddHttpMessageHandler<AuthRequestHandler>();           
        }
    }
}
