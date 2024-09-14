import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { NodesResponse } from '../models/response/nodes.response.model';
import { environment } from '../enviroments/enviroment';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class NodesService {
  private readonly endpoint = '/nodes';
  private readonly apiUrl = `${environment.apiUrl}${this.endpoint}`;

  constructor(private http: HttpClient) {}

  getNodes(): Promise<NodesResponse> {
    return lastValueFrom(this.http.get<NodesResponse>(this.apiUrl));
  }
}
