import { Routes } from '@angular/router';

import { StationsComponent } from './components/stations/stations.component';
import { ViewComponent } from './components/view/view.component';
import { NodesComponent } from './components/nodes/nodes.component';
import { SensorsComponent } from './components/sensors/sensors.component';
import { WeatherComponent } from './components/weather/weather.component';

export const routes: Routes = [
  { path: '', component: ViewComponent },
  { path: 'stations', component: StationsComponent },
  { path: 'nodes', component: NodesComponent },
  { path: 'sensors', component: SensorsComponent },
  { path: 'weather', component: WeatherComponent },
  // Add other routes as needed
];