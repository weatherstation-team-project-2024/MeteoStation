import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Sensor } from '../models/sensor.model';
import { Weather } from '../models/weather.model';
import { Station } from '../models/station.model';
import { Node } from '../models/node.model';
import { environment } from '../enviroments/enviroment';

@Injectable({
  providedIn: 'root'
})
export class DataStoreService {
  private readonly stationsEndpoint = '/stations';
  private readonly sensorsEndpoint = '/sensors';
  private readonly nodesEndpoint = '/nodes';
  private readonly weatherEndpoint = '/weather';

  private readonly stationsApiUrl = `${environment.apiUrl}${this.stationsEndpoint}`;
  private readonly sensorsApiUrl = `${environment.apiUrl}${this.sensorsEndpoint}`;
  private readonly nodesApiUrl = `${environment.apiUrl}${this.nodesEndpoint}`;
  private readonly weatherApiUrl = `${environment.apiUrl}${this.weatherEndpoint}`;
  private sensorsSubject = new BehaviorSubject<Sensor[]>([]);
  private weatherSubject = new BehaviorSubject<Weather[]>([]);
  private stationsSubject = new BehaviorSubject<Station[]>([]);
  private nodesSubject = new BehaviorSubject<Node[]>([]);

  sensors$ = this.sensorsSubject.asObservable();
  weather$ = this.weatherSubject.asObservable();
  stations$ = this.stationsSubject.asObservable();
  nodes$ = this.nodesSubject.asObservable();

  constructor(private http: HttpClient) {}

  fetchSensors() {
    if (this.sensorsSubject.value.length === 0) {
      this.http.get<{sensors: Sensor[]}>(this.sensorsApiUrl).subscribe({
        next: (response) => this.sensorsSubject.next(response.sensors),
        error: (error) => console.error('Error fetching sensors:', error)
      });
    }
  }

  fetchWeather() {
    if (this.weatherSubject.value.length === 0) {
      this.http.get<{weather: Weather[]}>(this.weatherApiUrl).subscribe({
        next: (response) => this.weatherSubject.next(response.weather),
        error: (error) => console.error('Error fetching weather:', error)
      });
    }
  }

  fetchStations() {
    if (this.stationsSubject.value.length === 0) {
      this.http.get<{stations: Station[]}>(this.stationsApiUrl).subscribe({
        next: (response) => this.stationsSubject.next(response.stations),
        error: (error) => console.error('Error fetching stations:', error)
      });
    }
  }

  fetchNodes() {
    if (this.nodesSubject.value.length === 0) {
      this.http.get<{nodes: Node[]}>(this.nodesApiUrl).subscribe({
        next: (response) => this.nodesSubject.next(response.nodes),
        error: (error) => console.error('Error fetching nodes:', error)
      });
    }
  }

  getSensors(): Observable<Sensor[]> {
    this.fetchSensors();
    return this.sensors$;
  }

  getWeather(): Observable<Weather[]> {
    this.fetchWeather();
    return this.weather$;
  }

  getStations(): Observable<Station[]> {
    this.fetchStations();
    return this.stations$;
  }

  getNodes(): Observable<Node[]> {
    this.fetchNodes();
    return this.nodes$;
  }
}