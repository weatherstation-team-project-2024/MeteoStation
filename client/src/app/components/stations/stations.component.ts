import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DataTablesModule } from 'angular-datatables';
import { StationsResponse } from '../../models/response/stations.response.model';
import { StationsService } from '../../services/stations.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Station } from '../../models/station.model';

@Component({
  selector: 'app-stations',
  standalone: true,
  imports: [CommonModule, DataTablesModule],
  templateUrl: './stations.component.html',
  styleUrls: ['./stations.component.css'],
})
export class StationsComponent implements OnInit {
  stations: Station[] = [];
  dtOptions: any = {};

  constructor(private stationsService: StationsService) {
    this.dtOptions = {
      pagingType: 'full_numbers',
      scrollX: true,
      columnDefs: [
        { className: 'dt-center', targets: '_all' }
      ]
    };
  }

  ngOnInit(): void {
    this.stationsService.getStations()
      .then((data: StationsResponse) => (this.stations = data.stations))
      .catch((error: HttpErrorResponse) => (console.error(`Failed to fetch: ${error.message}`)));
  }
}
