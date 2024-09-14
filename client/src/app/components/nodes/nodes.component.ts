import { CommonModule } from '@angular/common';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';

import { DataStoreService } from '../../services/datastore.service.ts.service';
import { Node } from '../../models/node.model';

@Component({
  selector: 'app-nodes',
  standalone: true,
  imports: [CommonModule, MatIconModule, MatTableModule],
  templateUrl: './nodes.component.html',
  styleUrls: ['./nodes.component.css'],
})
export class NodesComponent implements OnInit, OnDestroy {
  displayedColumns: (keyof Node)[] = [
    'node_id',
    'node_name',
    'device_id',
    'device_id_hex',
    'firmware_version',
    'active',
    'station_id',
    // 'station_id_uuid',
    'station_name',
    'latitude',
    'longitude',
    'elevation',
    'registered_date'
  ];
  dataSource: Node[] = [];
  private subscription: Subscription | null = null;

  constructor(private dataStore: DataStoreService) {}

  ngOnInit() {
    this.subscription = this.dataStore.getNodes().subscribe({
      next: (nodes) => {
        this.dataSource = nodes;
        console.log(this.dataSource);
      },
      error: (error) => console.error('Error fetching nodes:', error)
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