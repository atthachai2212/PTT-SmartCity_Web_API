using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public static class AppSettingService
    {
        public static string LoRaGateway {
            get {
                return Setting<string>("LoRaGateway");
            }
        }

        public static string GpsTracking {
            get {
                return Setting<string>("GpsTracking");
            }
        }

        public static string SensorHub {
            get {
                return Setting<string>("SensorHub");
            }
        }

        public static string WasteBinSensor {
            get {
                return Setting<string>("WasteBinSensor");
            }
        }

        public static string WaterLevelSensor {
            get {
                return Setting<string>("WaterLevelSensor");
            }
        }

        public static string EnvironmentSensor {
            get {
                return Setting<string>("EnvironmentSensor");
            }
        }

        public static string WaterQualitySensor {
            get {
                return Setting<string>("WaterQualitySensor");
            }
        }

        public static string WeatherSensor {
            get {
                return Setting<string>("WeatherSensor");
            }
        }
        private static T Setting<T>(string name)
        {
            string value = ConfigurationManager.AppSettings[name];

            if (value == null)
            {
                throw new Exception(String.Format("Could not find setting '{0}',", name));
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }
    }
}