export interface Sensor {
    lsid: number;
    modified_date: number;
    sensor_type: number;
    category: string;
    manufacturer: string;
    product_name: string;
    product_number: string;
    rain_collector_type: number;
    active: boolean;
    created_date: number;
    station_id: number;
    station_id_uuid: string;
    station_name: string;
    parent_device_type: string;
    parent_device_name: string;
    parent_device_id: number;
    parent_device_id_hex: string;
    port_number: number;
    latitude: number;
    longitude: number;
    elevation: number;
    tx_id: number | null;
}

