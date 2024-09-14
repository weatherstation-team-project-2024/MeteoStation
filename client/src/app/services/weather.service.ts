import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { WeatherResponse } from '../models/response/weather.response.model';
import { environment } from '../enviroments/enviroment';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WeatherService {
  private readonly endpoint = '/weather';
  private readonly apiUrl = `${environment.apiUrl}${this.endpoint}`;

  constructor(private http: HttpClient) {}

  getStations(): Promise<WeatherResponse> {
    return lastValueFrom(this.http.get<WeatherResponse>(this.apiUrl));
  }
}
