import { RouterOutlet, RouterLink } from '@angular/router';
import { Component } from '@angular/core';

@Component({
  selector: 'app-data-view',
  standalone: true,
  imports: [RouterOutlet, RouterLink],
  templateUrl: './data-view.component.html',
  styleUrls: ['./data-view.component.css']
})
export class DataViewComponent { }