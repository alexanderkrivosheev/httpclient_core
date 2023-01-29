using Weather.WeatherHttpClient;
using WorkerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Worker>();
        services.AddWeatherHttpClient();

        services.AddSingleton(sp => sp.GetRequiredService<ILoggerFactory>()
        .CreateLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType));

    })
    .Build();


await host.RunAsync();
