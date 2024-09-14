import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { SensorsResponse } from '../../models/response/sensors.response.model';
import { SensorsService } from '../../services/sensors.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Sensor } from '../../models/sensor.model';

@Component({
  selector: 'app-sensors',
  standalone: true,
  imports: [CommonModule, DataTablesModule],
  templateUrl: './sensors.component.html',
  styleUrls: ['./sensors.component.css'],
})
export class SensorsComponent implements OnInit {
  sensors: Sensor[] = [];
  dtOptions: any = {};

  constructor(private sensorsService: SensorsService) {
    this.dtOptions = {
      pagingType: 'full_numbers',
      scrollX: true,
      columnDefs: [
        { className: 'dt-center', targets: '_all' }
      ]
    };
  }

  ngOnInit(): void {
    this.sensorsService.getSensors()
      .then((data: SensorsResponse) => { console.log(data.sensors); this.sensors = data.sensors })
      .catch((error: HttpErrorResponse) => (console.error(`Failed to fetch: ${error.message}`)));
  }
}

