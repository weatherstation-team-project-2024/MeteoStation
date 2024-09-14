import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

declare var $: any;

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {

}
