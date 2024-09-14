import { Component, OnInit, OnDestroy } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { CommonModule } from '@angular/common';
import { Subscription } from 'rxjs';

import { DataStoreService } from '../../services/datastore.service.ts.service';
import { Sensor } from '../../models/sensor.model';

@Component({
  selector: 'app-sensors',
  standalone: true,
  imports: [CommonModule, DataTablesModule],
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.css'],
})
export class SensorsComponent implements OnInit, OnDestroy {
  sensors: Sensor[] = [];
  dtOptions: any = {};
  private subscription: Subscription | null = null;

  constructor(private dataStore: DataStoreService) {
    this.dtOptions = {
      pagingType: 'full_numbers',
      scrollX: true,
      columnDefs: [
        { className: 'dt-center', targets: '_all' }
      ]
    };
  }

  ngOnInit(): void {
    // this.subscription = this.dataStore.getSensors().subscribe({
    //   next: (sensors) => {
    //     this.sensors = sensors;
    //     if ($.fn.dataTable.isDataTable('#sensorsTable')) {
    //       $('#sensorsTable').DataTable().destroy();
    //     }
    //     setTimeout(() => {
    //       $('#sensorsTable').DataTable(this.dtOptions);
    //     });
    //   },
    //   error: (error) => console.error('Error fetching sensors:', error)
    // });
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
    // Destroy DataTable
    if ($.fn.dataTable.isDataTable('#sensorsTable')) {
      $('#sensorsTable').DataTable().destroy();
    }
  }
}