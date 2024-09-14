export interface Node {
  node_id: number;
  node_name: string;
  registered_date: number;
  device_id: number;
  device_id_hex: string;
  firmware_version: number;
  active: boolean;
  station_id: number;
  station_id_uuid: string;
  station_name: string;
  latitude: number;
  longitude: number;
  elevation: number;
}
  