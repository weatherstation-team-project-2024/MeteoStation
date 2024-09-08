using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using api.Models;
using api.Data;

namespace api.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
        private bool _initialized;
        private int _frequency;

        public Worker(
            ILogger<Worker> logger,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _serviceProvider = serviceProvider;
            _initialized = false;
            _frequency = 15; // todo: fetch "recording_interval": 15 
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await FetchDataFromCloud();
                await Task.Delay(TimeSpan.FromMinutes(_frequency), stoppingToken);
            }
        }

        private async Task FetchDataFromCloud()
        {
            _logger.LogInformation("Fetching data from cloud at: {time}", DateTimeOffset.Now);

            if (!_initialized)
            {
                Initialize();
                _initialized = true;
            }

            await FetchStations();
            await FetchSensors();

            await Task.CompletedTask;
        }

        private void Initialize()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    _context.Database.Migrate();
                    _context.Database.ExecuteSqlRaw($"SELECT create_hypertable('\"Sensors\"', 'ModifiedDate');");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while inicializing database and timescale extension.");
            }
        }

        private async Task FetchStations()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var client = _httpClientFactory.CreateClient();
                    var apiKey = _configuration["WeatherLinkApi:ApiKey"];
                    var apiSecret = _configuration["WeatherLinkApi:ApiSecret"];
                    var baseUrl = _configuration["WeatherLinkApi:BaseUrl"];

                    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(baseUrl))
                    {
                        _logger.LogError("API configuration is missing or incomplete.");

                        return;
                    }

                    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/stations?api-key={apiKey}");
                    request.Headers.Add("x-api-secret", apiSecret);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("API Response: {Content}", content);

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var stationsData = JsonSerializer.Deserialize<StationsResponse>(content, options);

                        if (stationsData == null || stationsData.Stations == null)
                        {
                            _logger.LogWarning("Deserialized weather data or stations is null.");
                            return;
                        }

                        foreach (var station in stationsData.Stations)
                        {
                            var existingStation = await _context.Stations.FirstOrDefaultAsync(s => s.StationIdUuid == station.StationIdUuid);

                            if (existingStation == null)
                            {
                                _context.Stations.Add(station);
                            }
                            else
                            {
                                _context.Entry(existingStation).CurrentValues.SetValues(station);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
            }

            await Task.CompletedTask;
        }

        private async Task FetchSensors()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();

                    var client = _httpClientFactory.CreateClient();
                    var apiKey = _configuration["WeatherLinkApi:ApiKey"];
                    var apiSecret = _configuration["WeatherLinkApi:ApiSecret"];
                    var baseUrl = _configuration["WeatherLinkApi:BaseUrl"];

                    if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret) || string.IsNullOrEmpty(baseUrl))
                    {
                        _logger.LogError("API configuration is missing or incomplete.");

                        return;
                    }

                    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/sensors?api-key={apiKey}");
                    request.Headers.Add("x-api-secret", apiSecret);

                    var response = await client.SendAsync(request);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        _logger.LogInformation("API Response: {Content}", content);

                        var options = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true
                        };

                        var sensorsData = JsonSerializer.Deserialize<SensorsResponse>(content, options);

                        if (sensorsData == null || sensorsData.Sensors == null)
                        {
                            _logger.LogWarning("Deserialized weather data or sensors is null.");
                            return;
                        }

                        foreach (var sensor in sensorsData.Sensors)
                        {
                            var existingSensor = await _context.Sensors.FirstOrDefaultAsync(s => s.Lsid == sensor.Lsid);

                            if (existingSensor == null)
                            {
                                _context.Sensors.Add(sensor);
                            }
                            else
                            {
                                _context.Entry(existingSensor).CurrentValues.SetValues(sensor);
                            }
                        }

                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while processing the request");
            }

            await Task.CompletedTask;
        }
    }
}