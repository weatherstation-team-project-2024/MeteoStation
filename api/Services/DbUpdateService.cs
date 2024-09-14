using Microsoft.EntityFrameworkCore;
using System.Text.Json;

using api.Models.ResponseModels;
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
                await Initialize();
                _initialized = true;
            }

            await FetchStations();
            await FetchNodes();
            await FetchSensors();
            await FetchWeatherData();

            await Task.CompletedTask;
        }

        private async Task Initialize()
        {
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<DataContext>();
                    _context.Database.Migrate();

                    var connection = _context.Database.GetDbConnection();
                    await connection.OpenAsync();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT EXISTS (SELECT 1 FROM timescaledb_information.hypertables WHERE hypertable_schema = 'public' AND hypertable_name = 'Sensors');";
                        #pragma warning disable CS8605 
                        var isSensorsHypertable = (bool)await command.ExecuteScalarAsync();
                        #pragma warning restore CS8605 

                        if (!isSensorsHypertable)
                        {
                            Console.WriteLine("Creating hypertable 'Sensors'...");
                            await _context.Database.ExecuteSqlRawAsync("SELECT create_hypertable('\"Sensors\"', 'ModifiedDate');");
                        }
                        else
                        {
                            Console.WriteLine("Hypertable 'Sensors' already exists.");
                        }
                    }

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "SELECT EXISTS (SELECT 1 FROM timescaledb_information.hypertables WHERE hypertable_schema = 'public' AND hypertable_name = 'Weather');";
                        #pragma warning disable CS8605 
                        var isWeatherHypertable = (bool)await command.ExecuteScalarAsync();
                        #pragma warning restore CS8605 

                        if (!isWeatherHypertable)
                        {
                            Console.WriteLine("Creating hypertable 'Weather'...");
                            await _context.Database.ExecuteSqlRawAsync("SELECT create_hypertable('\"Weather\"', 'Time');");
                        }
                        else{
                            Console.WriteLine("Hypertable 'Weather' already exists.");
                        }
                    }
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

        private async Task FetchNodes()
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

                    var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}/nodes?api-key={apiKey}");
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

                        var nodesData = JsonSerializer.Deserialize<NodeResponse>(content, options);

                        if (nodesData == null || nodesData.Nodes == null)
                        {
                            _logger.LogWarning("Deserialized nodes data is null.");
                            return;
                        }

                        foreach (var node in nodesData.Nodes)
                        {
                            var existingNode = await _context.Nodes.FirstOrDefaultAsync(n => n.NodeId == node.NodeId);

                            if (existingNode == null)
                            {
                                _context.Nodes.Add(node);
                            }
                            else
                            {
                                _context.Entry(existingNode).CurrentValues.SetValues(node);
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

        private async Task FetchWeatherData()
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

                    var requestUri = $"{baseUrl}/current/23c4b586-d9a7-4239-a25a-91804971903e?api-key={apiKey}";
                    var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
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

                        var weatherDataResponse = JsonSerializer.Deserialize<CurrentWeatherResponse>(content, options);

                        if (weatherDataResponse == null || weatherDataResponse.SensorList == null)
                        {
                            _logger.LogWarning("Deserialized weather data is null.");
                            return;
                        }

                        foreach (var weatherData in weatherDataResponse.SensorList)
                        {
                            foreach (var sensorData in weatherData.Data)
                            {
                                // DateTimeOffset now = DateTimeOffset.UtcNow;
                                // long unixTimestamp = now.ToUnixTimeSeconds();
                                // DateTime timeFetched = DateTimeOffset.FromUnixTimeSeconds(unixTimestamp).UtcDateTime;

                                var existingRecord = await _context.Weather
                                    .FirstOrDefaultAsync(w => w.Lsid == weatherData.Lsid && w.Time == sensorData.Time);

                                if (existingRecord != null)
                                {
                                }
                                else
                                {
                                    if (sensorData.Humidity != null && sensorData.Temperature != null)
                                    {
                                        _context.Weather.Add(new WeatherData
                                        {
                                            Lsid = weatherData.Lsid,
                                            // Time = timeFetched,
                                            Time = sensorData.Time,
                                            Humidity = sensorData.Humidity,
                                            Temperature = sensorData.Temperature,
                                            Pm2p5 = sensorData.Pm2p5
                                        });
                                    }
                                }
                            }
                        }

                        try
                        {
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateException ex)
                        {
                            Console.WriteLine($"An error occurred: {ex.Message}");

                            var sqlException = ex.InnerException as Exception;
                            if (sqlException != null)
                            {
                                Console.WriteLine($"SQL Error Message: {sqlException.Message}");
                            }
                        }
                        _logger.LogInformation("Weather data successfully updated.");
                    }
                    else
                    {
                        _logger.LogWarning("API request failed with status code: {StatusCode}", response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching weather data. Exception details: {ExceptionMessage}", ex.Message);
            }
        }
    }
}



