using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using PTT_SmartCity_Web_API.Models;
using Newtonsoft.Json;

namespace PTT_SmartCity_Web_API.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly IEnvironmentService environmentService;
        private readonly ISensorHubService sensorHubService;
        private readonly IWasteBinService wasteBinService;
        private readonly IWeatherService weatherService;
        private readonly IWaterService waterService;
        private readonly IGpsTrackingService gpsTrackingService;

        public ApplicationService()
        {
            this.environmentService = new EnvironmentService();
            this.sensorHubService = new SensorHubService();
            this.wasteBinService = new WasteBinService();
            this.weatherService = new WeatherService();
            this.waterService = new WaterService();
            this.gpsTrackingService = new GpsTrackingService();
        }


        public List<GetAppEnvironmentData> AppEnvironmentSensorItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var environmentSensorItems = this.environmentService.environmentSensorItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppEnvironmentData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                O2 = m.O2,
                O3 = m.O3,
                PM1 = m.PM1,
                PM2_5 = m.PM2_5,
                PM10 = m.PM10,
                AirTemp = m.AirTemp,
                AirHumidity = m.AirHumidity,
                AirPressure = m.AirPressure,
                BATLevel = m.BATLevel,
                BATVolt = m.BATVolt,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var environmentSensorData = new List<GetAppEnvironmentData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                environmentSensorData = environmentSensorItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                environmentSensorData = environmentSensorItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return environmentSensorData;
        }

        public List<GetAppGpsData> AppGspTrackingItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var gpsTrackingItems = this.gpsTrackingService.gpsItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppGpsData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                Emergency = string.Empty,
                Battery = m.Battery,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var gpsTrackingData = new List<GetAppGpsData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                gpsTrackingData = gpsTrackingItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                gpsTrackingData = gpsTrackingItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }

            return gpsTrackingData;

        }

        public List<GetAppSensorHubData> AppSensorHubItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var sensorHubItems = this.sensorHubService.sensorHubItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppSensorHubData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                Humidity = m.Humidity,
                Temperature = m.Temperature,
                CO2 = m.CO2,
                BATVolt = m.BATVolt,
                BATCurrent = m.BATCurrent,
                BATLevel = m.BATLevel,
                BATTemp = m.BATTemp,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var sensorHubData = new List<GetAppSensorHubData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                sensorHubData = sensorHubItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                sensorHubData = sensorHubItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return sensorHubData;
        }

        public List<GetAppWasteBinData> AppWasteBinSensorItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var wasteBinSensorItems = this.wasteBinService.wasteBinSensorItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppWasteBinData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                Full = m.Full,
                Flame = m.Flame,
                AirLevel = m.AirLevel,
                Battery = m.Battery,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var wasteBinSensorData = new List<GetAppWasteBinData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                wasteBinSensorData = wasteBinSensorItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                wasteBinSensorData = wasteBinSensorItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return wasteBinSensorData;
        }

        public List<GetAppWaterLevelData> AppWaterLevelSensorItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var waterLevelSensorItems = this.waterService.waterLevelSensorItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppWaterLevelData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                WaterLevel = m.WaterLevel,
                Distance = m.Distance,
                BATVolt = m.BATVolt,
                BATLevel = m.BATLevel,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var waterLevelSensorData = new List<GetAppWaterLevelData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                waterLevelSensorData = waterLevelSensorItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                waterLevelSensorData = waterLevelSensorItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return waterLevelSensorData;
        }

        public List<GetAppWaterQualityData> AppWaterQualitySensorItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var waterQualitySensorItems = this.waterService.waterQualitySensorItemsFilter(sDateTime.Year,eDateTime.Year).Select(m => new GetAppWaterQualityData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                WaterTemp = m.WaterTemp,
                DO = m.DO,
                DO_Cal = m.DO_Cal,
                BATVolt = m.BATVolt,
                BATLevel = m.BATLevel,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var waterQualitySensorData = new List<GetAppWaterQualityData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                waterQualitySensorData = waterQualitySensorItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                waterQualitySensorData = waterQualitySensorItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return waterQualitySensorData;

        }

        public List<GetAppWeatherData> AppWeatherSensorItems(ApplicationDataFilterOptions filterOptions)
        {
            DateTime sDateTime = Convert.ToDateTime(filterOptions.dateTimeStart);
            DateTime eDateTime = Convert.ToDateTime(filterOptions.dateTimeEnd);

            var weatherSensorItems = this.weatherService.weatherSensorItemsFilter(sDateTime.Year, eDateTime.Year).Select(m => new GetAppWeatherData
            {
                DateTime = Convert.ToDateTime(string.Format($"{m.Date.ToString("yyyy/MM/dd")} {m.Time.ToString("hh\\:mm\\:ss")}")),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                WindSpeed = m.WindSpeed,
                WindVane = m.WindVane,
                RainfallCurrentHour = m.RainfallCurrentHour,
                RainfallPreviousHour = m.RainfallPreviousHour,
                RainfallLast_24Hours = m.RainfallLast_24Hours,
                Luminosity = m.Luminosity,
                BATVolt = m.BATVolt,
                BATLevel = m.BATLevel,
                RSSI = m.RSSI,
                SNR = m.SNR
            }).OrderByDescending(x => x.DateTime).ToList();

            var weatherSensorData = new List<GetAppWeatherData>();
            if (!string.IsNullOrEmpty(filterOptions.deviceEUI))
            {
                weatherSensorData = weatherSensorItems.Where(m =>
                    m.DevEUI == filterOptions.deviceEUI &&
                    m.DateTime >= sDateTime &&
                    m.DateTime <= eDateTime
                    ).ToList();
            }
            else
            {
                weatherSensorData = weatherSensorItems.Where(m =>
                     m.DateTime >= sDateTime &&
                     m.DateTime <= eDateTime
                     ).ToList();
            }
            return weatherSensorData;
        }
    }
}