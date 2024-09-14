import { Weather } from '../weather.model';

export interface WeatherResponse {
  weather: Weather[];
  generatedAt: number;
}