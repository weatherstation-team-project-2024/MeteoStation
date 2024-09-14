import { HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { StationsResponse } from '../models/response/stations.response.model';
import { environment } from '../enviroments/enviroment';
import { lastValueFrom } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class StationsService {
  private readonly endpoint = '/stations';
  private readonly apiUrl = `${environment.apiUrl}${this.endpoint}`;

  constructor(private http: HttpClient) {}

  getStations(): Promise<StationsResponse> {
    return lastValueFrom(this.http.get<StationsResponse>(this.apiUrl));
  }
}
