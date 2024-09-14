import { RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';

import { StationsComponent } from './components/stations/stations.component';
import { DataStoreService } from './services/datastore.service.ts.service';
import { DataTablesModule } from 'angular-datatables';

@Component({
  selector: 'app-root',
  standalone: true,
  schemas: [],
  imports: [RouterOutlet, StationsComponent, DataTablesModule], 
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'client';

  constructor(private dataStore: DataStoreService) {}

  ngOnInit() {
    // Initialize all data
    this.dataStore.fetchSensors();
    this.dataStore.fetchWeather();
    this.dataStore.fetchStations();
    this.dataStore.fetchNodes();
  }
}