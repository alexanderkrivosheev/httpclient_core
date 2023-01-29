using System.Diagnostics.Tracing;
using System.Net.Http;
using Weather.WeatherHttpClient;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        class NetEventListener : EventListener
        {
            protected override void OnEventSourceCreated(EventSource eventSource)
            {
                if (eventSource.Name.StartsWith("System.Net"))
                    EnableEvents(eventSource, EventLevel.Informational);
            }
            protected override void OnEventWritten(EventWrittenEventArgs eventData)
            {
                if (eventData.EventName == "ResolutionStart")
                {
                    Console.WriteLine(eventData.EventName + " - " + eventData.Payload[0]);
                }
                else if (eventData.EventName == "RequestStart")
                {
                    Console.WriteLine(eventData.EventName + " - " + eventData.Payload[1]);
                }
            }
        }

        private readonly ILogger<Worker> _logger;
        private readonly IWeatherHttpClient _weatherHttpClient;

        public Worker(
            ILogger<Worker> logger, 
            IWeatherHttpClient weatherHttpClient)
        {
            _logger = logger;
            _weatherHttpClient = weatherHttpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _ = new NetEventListener();

            var timer = new PeriodicTimer(TimeSpan.FromMilliseconds(100));
            while (!stoppingToken.IsCancellationRequested &&  await timer.WaitForNextTickAsync())
            {
                Parallel.For(0, 100, async _ => await _weatherHttpClient.GetForecastAsync("London", DateTime.Now.AddDays(1)));              
            }
        }
    }
}