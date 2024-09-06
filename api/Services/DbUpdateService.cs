using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Models;

namespace api.Services
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;
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
            
            await FetchStations();

            await Task.CompletedTask;
        }

        private async Task FetchStations(){
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

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

                        var weatherData = JsonSerializer.Deserialize<WeatherDataResponse>(content, options);

                        if (weatherData == null || weatherData.Stations == null)
                        {
                            _logger.LogWarning("Deserialized weather data or stations is null.");
                            return;
                        }

                        foreach (var station in weatherData.Stations)
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
    }
}