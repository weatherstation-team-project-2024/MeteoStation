import { Station } from '../station.model';

export interface StationsResponse {
  stations: Station[];
  generated_at: number;
}