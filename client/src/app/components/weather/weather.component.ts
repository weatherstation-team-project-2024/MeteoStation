import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { HttpErrorResponse } from '@angular/common/http';
import { WeatherService } from '../../services/weather.service';
import { WeatherResponse } from '../../models/response/weather.response.model';
import { Weather } from '../../models/weather.model';

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [CanvasJSAngularChartsModule, CommonModule],
  template: `
    <div *ngIf="error">{{ error }}</div>
    <div *ngIf="debugInfo">
      <h3>Debug Info:</h3>
      <pre>{{ debugInfo | json }}</pre>
    </div>
    <canvasjs-chart *ngIf="chartOptions" [options]="chartOptions"></canvasjs-chart>
  `,
  styleUrls: ['./weather.component.css'],
})
export class WeatherComponent implements OnInit {
  weather: Weather[] = [];
  filteredWeather: Weather[] = [];
  chartOptions: any;
  error: string | null = null;
  debugInfo: any = {};

  constructor(private weatherService: WeatherService) {}

  ngOnInit(): void {
    this.fetchWeatherData();
  }

  fetchWeatherData(): void {
    this.weatherService.getStations()
      .then((data: WeatherResponse) => {
        this.weather = data.weather;
        this.debugInfo.totalDataPoints = this.weather.length;
        this.debugInfo.firstDataPoint = this.weather[0];
        this.debugInfo.lastDataPoint = this.weather[this.weather.length - 1];
        console.log('Weather data:', this.weather);
        this.filterData();
        if (this.filteredWeather.length > 0) {
          this.processData();
        } else {
          this.error = 'No weather data available for the selected date range.';
        }
      })
      .catch((error: HttpErrorResponse) => {
        console.error(`Failed to fetch: ${error.message}`);
        this.error = `Failed to fetch weather data: ${error.message}`;
      });
  }

  filterData(): void {
    // Get the timestamp of 7 days ago
    const sevenDaysAgo = Date.now() / 1000 - (7 * 24 * 60 * 60);
    
    // Sort the weather data by timestamp in descending order
    const sortedWeather = this.weather.sort((a, b) => b.ts - a.ts);
    
    // Take the last 7 days of data
    this.filteredWeather = sortedWeather.filter(item => item.ts >= sevenDaysAgo);

    this.debugInfo.startDate = new Date(sevenDaysAgo * 1000).toISOString();
    this.debugInfo.endDate = new Date().toISOString();
    this.debugInfo.filteredDataPoints = this.filteredWeather.length;
    this.debugInfo.firstFilteredDataPoint = this.filteredWeather[0];
    this.debugInfo.lastFilteredDataPoint = this.filteredWeather[this.filteredWeather.length - 1];

    console.log('Filtered weather data:', this.filteredWeather);
  }

  processData(): void {
    if (this.filteredWeather.length === 0) {
      console.warn('No filtered weather data to process');
      this.error = 'No data available for the selected date range.';
      return;
    }

    const temperatureData = this.filteredWeather.map(item => ({
      x: new Date(item.ts * 1000),
      y: item.temp
    }));

    const humidityData = this.filteredWeather.map(item => ({
      x: new Date(item.ts * 1000),
      y: item.hum
    }));

    this.chartOptions = {
      animationEnabled: true,
      theme: "light2",
      title: {
        text: "Temperature and Humidity (Last 7 Days)"
      },
      axisX: {
        title: "Time",
        valueFormatString: "DD MMM HH:mm"
      },
      axisY: {
        title: "Temperature (°F)"
      },
      axisY2: {
        title: "Humidity (%)",
        includeZero: false
      },
      toolTip: {
        shared: true
      },
      legend: {
        cursor: "pointer",
        itemclick: function (e: any) {
          if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
            e.dataSeries.visible = false;
          } else {
            e.dataSeries.visible = true;
          }
          e.chart.render();
        }
      },
      data: [{
        type: "line",
        name: "Temperature",
        showInLegend: true,
        dataPoints: temperatureData
      }, {
        type: "line",
        name: "Humidity",
        axisYType: "secondary",
        showInLegend: true,
        dataPoints: humidityData
      }]
    };

    this.debugInfo.chartDataPoints = {
      temperature: temperatureData.length,
      humidity: humidityData.length
    };

    console.log('Chart options set:', this.chartOptions);
  }
}