import { Component } from '@angular/core';

import { StationsComponent } from '../stations/stations.component';

@Component({
  selector: 'app-data-view',
  standalone: true,
  imports: [StationsComponent],
  templateUrl: './data-view.component.html',
  styleUrl: './data-view.component.css'
})
export class DataViewComponent {

}
