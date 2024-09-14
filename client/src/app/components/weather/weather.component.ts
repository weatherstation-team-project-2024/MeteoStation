import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { CanvasJSAngularChartsModule } from '@canvasjs/angular-charts';
import { Subscription } from 'rxjs';

import { Weather } from '../../models/weather.model';
import { DataStoreService } from '../../services/datastore.service.ts.service';

@Component({
  selector: 'app-weather',
  standalone: true,
  imports: [
    CanvasJSAngularChartsModule,
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule
  ],
  template: `
    <div class="weather-container">
      <mat-card class="time-selector">
        <mat-card-content>
          <mat-form-field>
            <mat-label>Minutes</mat-label>
            <input matInput type="number" [(ngModel)]="timeInput.minutes" min="0">
          </mat-form-field>
          <mat-form-field>
            <mat-label>Days</mat-label>
            <input matInput type="number" [(ngModel)]="timeInput.days" min="0">
          </mat-form-field>
          <mat-form-field>
            <mat-label>Months</mat-label>
            <input matInput type="number" [(ngModel)]="timeInput.months" min="0">
          </mat-form-field>
          <mat-form-field>
            <mat-label>Years</mat-label>
            <input matInput type="number" [(ngModel)]="timeInput.years" min="0">
          </mat-form-field>
          <button mat-raised-button color="primary" (click)="applyTimeSelection()">Apply</button>
        </mat-card-content>
      </mat-card>

      <mat-card class="average-values">
        <mat-card-content>
          <h2>Average Values</h2>
          <p>Temperature: {{ averageTemp }}°F</p>
          <p>Humidity: {{ averageHum }}%</p>
        </mat-card-content>
      </mat-card>

      <div class="chart-container">
        <canvasjs-chart [options]="chartOptions" [styles]="{width: '100%', height: '400px'}"></canvasjs-chart>
      </div>

      <mat-card *ngIf="error" class="error-message">
        <mat-card-content>
          {{ error }}
        </mat-card-content>
      </mat-card>
    </div>
  `,
  styles: [`
    .weather-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 20px;
      max-width: 800px;
      margin: 0 auto;
    }
    .time-selector, .average-values, .chart-container, .error-message {
      width: 100%;
      margin-bottom: 20px;
    }
    .time-selector mat-form-field {
      margin-right: 10px;
    }
    .chart-container {
      height: 400px;
    }
    .error-message {
      color: red;
    }
  `]
})
export class WeatherComponent implements OnInit, OnDestroy {
  weather: Weather[] = [];
  filteredWeather: Weather[] = [];
  chartOptions: any;
  error: string | null = null;
  averageTemp: number = 0;
  averageHum: number = 0;
  timeInput = {
    minutes: 0,
    days: 7,
    months: 0,
    years: 0
  };
  private subscription: Subscription | null = null;

  constructor(private dataStore: DataStoreService) {}

  ngOnInit(): void {
    this.loadData();
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  loadData(): void {
    this.subscription = this.dataStore.getWeather().subscribe({
      next: (weather) => {
        this.weather = weather;
        this.applyTimeSelection();
      },
      error: (error) => {
        console.error('Error fetching weather data:', error);
        this.error = `An error occurred: ${error.message}`;
      }
    });
  }

  applyTimeSelection(): void {
    const now = new Date();
    const pastDate = new Date(now.getTime());
    pastDate.setMinutes(pastDate.getMinutes() - this.timeInput.minutes);
    pastDate.setDate(pastDate.getDate() - this.timeInput.days);
    pastDate.setMonth(pastDate.getMonth() - this.timeInput.months);
    pastDate.setFullYear(pastDate.getFullYear() - this.timeInput.years);

    const startTime = pastDate.getTime() / 1000;
    const endTime = now.getTime() / 1000;

    this.filteredWeather = this.weather
      .sort((a, b) => b.ts - a.ts)
      .filter(item => item.ts >= startTime && item.ts <= endTime);

    this.calculateAverages();
    this.processData();
  }

  calculateAverages(): void {
    if (this.filteredWeather.length === 0) {
      this.averageTemp = 0;
      this.averageHum = 0;
      return;
    }

    const sum = this.filteredWeather.reduce((acc, item) => {
      return {
        temp: acc.temp + (item.temp ?? 0),
        hum: acc.hum + (item.hum ?? 0)
      };
    }, { temp: 0, hum: 0 });

    this.averageTemp = +(sum.temp / this.filteredWeather.length).toFixed(2);
    this.averageHum = +(sum.hum / this.filteredWeather.length).toFixed(2);
  }

  processData(): void {
    if (this.filteredWeather.length === 0) {
      console.warn('No filtered weather data to process');
      this.error = 'No data available for the selected time range.';
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
        text: "Temperature and Humidity"
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
  }
}