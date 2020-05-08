using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class ApplicationDataModel
    {
    }

    public class GetAppEnvironmentData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float O2 { get; set; }
        public float O3 { get; set; }
        public float PM1 { get; set; }
        public float PM2_5 { get; set; }
        public float PM10 { get; set; }
        public float AirTemp { get; set; }
        public float AirHumidity { get; set; }
        public float AirPressure { get; set; }
        public float BATLevel { get; set; }
        public float BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppSensorHubData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float Humidity { get; set; }
        public float Temperature { get; set; }
        public float CO2 { get; set; }
        public float? BATVolt { get; set; }
        public float? BATCurrent { get; set; }
        public float? BATLevel { get; set; }
        public float? BATTemp { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppWasteBinData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public bool Full { get; set; }
        public bool Flame { get; set; }
        public float AirLevel { get; set; }
        public float? Battery { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppWeatherData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WindSpeed { get; set; }
        public float WindVane { get; set; }
        public float RainfallCurrentHour { get; set; }
        public float RainfallPreviousHour { get; set; }
        public float RainfallLast_24Hours { get; set; }
        public float Luminosity { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppWaterLevelData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WaterLevel { get; set; }
        public float Distance { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppWaterQualityData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WaterTemp { get; set; }
        public float DO { get; set; }
        public float DO_Cal { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetAppGpsData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Emergency { get; set; }
        public float? Battery { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class ApplicationDataFilterOptions
    {
        public string deviceType { get; set; }
        public string deviceEUI { get; set; }
        public string dateTimeStart { get; set; }
        public string dateTimeEnd { get; set; }
    }
}