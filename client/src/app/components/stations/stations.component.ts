import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

import { DataStoreService } from '../../services/datastore.service.ts.service';
import { Station } from '../../models/station.model';

@Component({
  selector: 'app-stations',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatTableModule],
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css'],
})
export class StationsComponent implements OnInit, OnDestroy {
  displayedColumns: string[] = [
    'station_id',
    'station_name',
    'product_number',
    'gateway_id',
    'gateway_id_hex',
    'user_email',
    'company_name',
    'active',
    'private',
    'recording_interval',
    'firmware_version',
    'registered_date',
    'time_zone',
    'city',
    'region',
    'country',
    'latitude',
    'longitude',
    'elevation',
    'gateway_type',
    'relationship_type',
    'subscription_type'
  ]
  dataSource: Station[] = [];
  private subscription: Subscription | null = null;

  constructor(private dataStore: DataStoreService) {
    this.dataStore.fetchStations();
  }

  ngOnInit() {
    this.subscription = this.dataStore.getStations().subscribe({
      next: (stations) => {
        this.dataSource = stations;
        console.log(this.dataSource);
      },
      error: (error) => console.error('Error fetching stations:', error)
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