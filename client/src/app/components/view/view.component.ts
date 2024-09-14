import { RouterOutlet } from '@angular/router';
import { Component } from '@angular/core';

import { DataViewComponent } from '../data-view/data-view.component';
import { SidebarComponent } from '../sidebar/sidebar.component';

@Component({
  selector: 'app-view',
  standalone: true,
  imports: [RouterOutlet, SidebarComponent, DataViewComponent],
  templateUrl: './view.component.html',
  styleUrl: './view.component.css'
})
export class ViewComponent {

}
