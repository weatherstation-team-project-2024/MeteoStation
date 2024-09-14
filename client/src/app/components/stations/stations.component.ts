import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy, ViewChild, AfterViewInit } from '@angular/core';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatSortModule, MatSort } from '@angular/material/sort';
import { MatPaginatorModule, MatPaginator } from '@angular/material/paginator';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatCardModule } from '@angular/material/card';
import { FormsModule } from '@angular/forms';
import { Subscription } from 'rxjs';

import { Station } from '../../models/station.model';
import { DataStoreService } from '../../services/datastore.service.ts.service';

@Component({
  selector: 'app-stations',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatSortModule,
    MatPaginatorModule,
    MatInputModule,
    MatFormFieldModule,
    MatCardModule,
    FormsModule
  ],
  template: `
  <section class="example-container mat-elevation-z8" tabindex="0">
    <mat-card class="mat-elevation-z8">
      <mat-card-header>
        <mat-card-title>Stations</mat-card-title>
      </mat-card-header>
      <mat-card-content>
        <mat-form-field>
          <input matInput (keyup)="applyFilter($event)" placeholder="Filter" #input>
        </mat-form-field>

        <div class="table-container mat-elevation-z8">
          <table mat-table [dataSource]="dataSource" matSort>
          <ng-container *ngFor="let column of displayedColumns" [matColumnDef]="column.toString()">
              <th mat-header-cell *matHeaderCellDef mat-sort-header> {{ column.toString() | titlecase }} </th>
              <td mat-cell *matCellDef="let station">
                <ng-container [ngSwitch]="column">
                  <ng-container *ngSwitchCase="'registered_date'">
                    {{formatDate(station[column]) || 'NULL'}}
                  </ng-container>
                  <ng-container *ngSwitchCase="'active'">
                    {{station[column] ? 'Yes' : 'No'}}
                  </ng-container>
                  <ng-container *ngSwitchDefault>
                    {{station[column] || 'NULL'}}
                  </ng-container>
                </ng-container>
              </td>
            </ng-container>

            <tr mat-header-row *matHeaderRowDef="displayedColumns; sticky: true"></tr>
            <tr mat-row *matRowDef="let row; columns: displayedColumns;"></tr>

            <tr class="mat-row" *matNoDataRow>
              <td class="mat-cell" colspan="4">No data matching the filter "{{input.value}}"</td>
            </tr>
          </table>
        </div>

        <mat-paginator [pageSizeOptions]="[5, 10, 25, 100]" aria-label="Select page of stations"></mat-paginator>
      </mat-card-content>
    </mat-card>
  </section>
  `,
  styles: [`
    .mat-mdc-card-content {
      padding: 16px;
    }

    .mat-mdc-form-field {
      font-size: 14px;
      width: 100%;
    }

    .table-container {
      overflow-x: auto;
      max-width: 100%;
    }

    table {
      width: 100%;
    }

    .mat-mdc-card-header {
      background-color: #f5f5f5;
      padding: 16px;
    }

    .mat-mdc-card-title {
      font-size: 20px;
      font-weight: 500;
    }

    .mat-column-active {
      max-width: 80px;
    }

    .mat-mdc-row:hover {
      background-color: #f5f5f5;
    }

    .mat-mdc-header-row {
      position: sticky;
      top: 0;
      z-index: 100;
      background-color: white;
    }
  `]
})
export class StationsComponent implements OnInit, OnDestroy, AfterViewInit {
  displayedColumns: (keyof Station)[] = [
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
  ];

  dataSource: MatTableDataSource<Station>;
  private subscription: Subscription | null = null;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private dataStore: DataStoreService) {
    this.dataSource = new MatTableDataSource<Station>([]);
  }

  ngOnInit() {
    this.subscription = this.dataStore.getStations().subscribe({
      next: (stations) => {
        this.dataSource.data = stations;
      },
      error: (error) => console.error('Error fetching stations:', error)
    });
  }

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
    this.dataSource.sort = this.sort;
  }

  ngOnDestroy(): void {
    if (this.subscription) {
      this.subscription.unsubscribe();
    }
  }

  formatDate(timestamp: number): string {
    return new Date(timestamp).toLocaleString();
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
}
