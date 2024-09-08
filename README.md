# MeteoStation

### 1. Database Setup

```copy
CREATE DATABASE "MeteoStations";
\c "MeteoStations";
CREATE ROLE timskiprojekat2024 WITH PASSWORD 'timskiprojekat2024';
ALTER USER timskiprojekat2024 WITH SUPERUSER;
CREATE EXTENSION IF NOT EXISTS timescaledb;
```