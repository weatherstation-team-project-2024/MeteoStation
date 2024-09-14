import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { lastValueFrom } from 'rxjs';

import { SensorsResponse } from '../models/response/sensors.response.model';
import { environment } from '../enviroments/enviroment';

@Injectable({
  providedIn: 'root',
})
export class SensorsService {
  private readonly endpoint = '/sensors';
  private readonly apiUrl = `${environment.apiUrl}${this.endpoint}`;

  constructor(private http: HttpClient) {}

  async getSensors(): Promise<SensorsResponse> {
    return lastValueFrom(this.http.get<SensorsResponse>(this.apiUrl));
  }
}

