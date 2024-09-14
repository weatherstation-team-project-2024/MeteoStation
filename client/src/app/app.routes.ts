import { Routes } from '@angular/router';
import { WeatherComponent } from './components/weather/weather.component';
import { StationsComponent } from './components/stations/stations.component';
import { NodesComponent } from './components/nodes/nodes.component';
import { SensorsComponent } from './components/sensors/sensors.component';
import { ViewComponent } from './components/view/view.component';

export const routes: Routes = [
  {
    path: '',
    component: ViewComponent,
    children: [
      { path: '', redirectTo: 'weather', pathMatch: 'full' },
      { path: 'weather', component: WeatherComponent },
      { path: 'stations', component: StationsComponent },
      { path: 'nodes', component: NodesComponent },
      { path: 'sensors', component: SensorsComponent },
    ]
  },
  { path: '**', redirectTo: 'weather' }  // Change this line
];