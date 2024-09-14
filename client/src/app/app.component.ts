import { RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';

import { StationsComponent } from './components/stations/stations.component';
import { DataStoreService } from './services/datastore.service.ts.service';

@Component({
  selector: 'app-root',
  standalone: true,
  schemas: [],
  imports: [RouterOutlet, StationsComponent], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'client';

  constructor(private dataStore: DataStoreService) {}

  ngOnInit() {
    this.dataStore.fetchSensors();
    this.dataStore.fetchWeather();
    this.dataStore.fetchStations();
    this.dataStore.fetchNodes();
  }
}