import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { DataTablesModule } from 'angular-datatables';
import { NodesResponse } from '../../models/response/nodes.response.model';
import { NodesService } from '../../services/nodes.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Node } from '../../models/node.model';

@Component({
  selector: 'app-nodes',
  standalone: true,
  imports: [CommonModule, DataTablesModule],
  templateUrl: './nodes.component.html',
  styleUrls: ['./nodes.component.css'],
})
export class NodesComponent implements OnInit {
  nodes: Node[] = [];
  dtOptions: any = {};

  constructor(private nodesService: NodesService) {
    this.dtOptions = {
      pagingType: 'full_numbers',
      scrollX: true,
      columnDefs: [
        { className: 'dt-center', targets: '_all' }
      ],
    };
  }

  ngOnInit(): void {
    this.nodesService.getNodes()
      .then((data: NodesResponse) => { console.log(data.nodes); this.nodes = data.nodes; })
      .catch((error: HttpErrorResponse) => (console.error(`Failed to fetch: ${error.message}`)));
  }
}
