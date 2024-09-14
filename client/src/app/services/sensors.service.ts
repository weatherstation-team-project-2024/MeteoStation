import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, lastValueFrom } from 'rxjs';

import { SensorsResponse } from '../models/response/sensors.response.model';
import { environment } from '../enviroments/enviroment';
import { Sensor } from '../models/sensor.model';

@Injectable({
  providedIn: 'root',
})
export class SensorsService {
  private readonly endpoint = '/sensors';
  private readonly apiUrl = `${environment.apiUrl}${this.endpoint}`;
  private sensorsSubject = new BehaviorSubject<Sensor[]>([]);
  sensors$ = this.sensorsSubject.asObservable();

  constructor(private http: HttpClient) { }

  getSensors(): Observable<Sensor[]> {
    if (this.sensorsSubject.value.length === 0) {
      this.fetchSensors();
    }
    return this.sensors$;
  }

  private fetchSensors(): void {
    this.http.get<SensorsResponse>(this.apiUrl).subscribe({
      next: (response) => this.sensorsSubject.next(response.sensors),
      error: (error) => console.error('Error fetching sensors:', error)
    });
  }

  // async getSensors(): Promise<SensorsResponse> {
  //   return lastValueFrom(this.http.get<SensorsResponse>(this.apiUrl));
  // }
}

