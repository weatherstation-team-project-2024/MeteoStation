import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

import { DataStoreService } from '../../services/datastore.service.ts.service';
import { Sensor } from '../../models/sensor.model';

@Component({
  selector: 'app-sensors',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatTableModule],
  providers: [],
  templateUrl: './sensors.component.html',
  styleUrl: './sensors.component.css'
})
export class SensorsComponent implements OnInit, OnDestroy {
  displayedColumns: (keyof Sensor)[] = [
    'lsid',
    'modified_date',
    'sensor_type',
    'category',
    'manufacturer',
    'product_name',
    'product_number',
    'rain_collector_type',
    'active',
    'created_date',
    'station_id',
    // 'station_id_uuid',
    'station_name',
    'parent_device_type',
    'parent_device_name',
    'parent_device_id',
    'parent_device_id_hex',
    'port_number',
    'latitude',
    'longitude',
    'elevation',
    'tx_id'
  ];
  dataSource: Sensor[] = [];
  private subscription: Subscription | null = null;

  constructor(private dataStore: DataStoreService) {
    this.dataStore.fetchSensors();
  }

  ngOnInit() {
    this.subscription = this.dataStore.getSensors().subscribe({
      next: (sensors) => {
        this.dataSource = sensors;
        sensors.forEach(element => {
          this.dataSource.push(element);
        });
        console.log(this.dataSource);
      },
      error: (error) => console.error('Error fetching sensors:', error)
    });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  formatDate(timestamp: number): string {
    return new Date(timestamp).toLocaleString();
  }
}