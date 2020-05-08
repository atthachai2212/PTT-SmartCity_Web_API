using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class ApplicationController : Controller
    {
        private IApplicationService applicationService;
        private ILoRaDeviceSettingService loRaDeviceSettingService;

        public ApplicationController()
        {
            this.applicationService = new ApplicationService();
            this.loRaDeviceSettingService = new LoRaDeviceSettingService();
        }

        // GET: Application
        public ActionResult EnvironmentSensor()
        {
            ViewBag.AppTitle = "Environment Sensor";
            ViewBag.AppTitleIcon = "fa fa-envira";
            ViewBag.AppSubtitle = "SmartCity Environment Sensor";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "Environment Sensor";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.EnvironmentSensor).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.EnvironmentSensor)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }
        public ActionResult GpsTracking()
        {
            ViewBag.AppTitle = "Gps Tracking";
            ViewBag.AppTitleIcon = "fa fa-podcast";
            ViewBag.AppSubtitle = "SmartCity Gps Tracking";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "Gps Tracking";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.GpsTracking).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.GpsTracking)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }

        public ActionResult SensorHub()
        {
            ViewBag.AppTitle = "Sensor Hub";
            ViewBag.AppTitleIcon = "fa fa-sitemap";
            ViewBag.AppSubtitle = "SmartCity Sensor Hub";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "Sensor Hub";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.SensorHub).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.SensorHub)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }

        public ActionResult WasteBinSensor()
        {
            ViewBag.AppTitle = "WasteBin Sensor";
            ViewBag.AppTitleIcon = "fa fa-refresh";
            ViewBag.AppSubtitle = "SmartCity WasteBin Sensor";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "WasteBin Sensor";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.WasteBinSensor).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.WasteBinSensor)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }

        public ActionResult WaterLevelSensor()
        {
            ViewBag.AppTitle = "WaterLevel Sensor";
            ViewBag.AppTitleIcon = "fa fa-thermometer-empty";
            ViewBag.AppSubtitle = "SmartCity WaterLevel Sensor";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "WaterLevel Sensor";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.WaterLevelSensor).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.WaterLevelSensor)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }

        public ActionResult WaterQualitySensor()
        {
            ViewBag.AppTitle = "WaterQuality Sensor";
            ViewBag.AppTitleIcon = "fa fa-thermometer-full";
            ViewBag.AppSubtitle = "SmartCity WaterQuality Sensor";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "WaterQuality Sensor";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.WaterQualitySensor).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.WaterQualitySensor)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }


        public ActionResult WeatherSensor()
        {
            ViewBag.AppTitle = "Weather Sensor";
            ViewBag.AppTitleIcon = "fa fa-snowflake-o";
            ViewBag.AppSubtitle = "SmartCity Weather Sensor";
            ViewBag.AppBreadcrumbMemu = "Applications";
            ViewBag.AppBreadcrumbItem = "Weather Sensor";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-windows";

            var devTypeConut = this.loRaDeviceSettingService.loraDeviceItems.GroupBy(g => g.DevType).Select(s => new { DevType = s.Key, Count = s.Count() }).ToList();
            var devEUI = this.loRaDeviceSettingService.loraDeviceItems.Where(x => x.DevType == AppSettingService.WeatherSensor).GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.devTypeConut = devTypeConut.SingleOrDefault(x => x.DevType == AppSettingService.WeatherSensor)?.Count ?? 0;
            ViewBag.devEUI = devEUI;
            return View();
        }

        [HttpGet]
        public JsonResult GetLoRaWANData(ApplicationDataFilterOptions filterOptions)
        {
            var jsonResult = new JsonResult();
            if (!string.IsNullOrEmpty(filterOptions.deviceType))
            {              
                switch (filterOptions.deviceType)
                {
                    case "EnvironmentSensor":
                        var environmentSensorItems = this.applicationService.AppEnvironmentSensorItems(filterOptions).Select(m => new {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
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

                        });
                        jsonResult = Json(new { data = environmentSensorItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "SensorHub":
                        var sensorHubItems = this.applicationService.AppSensorHubItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
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
                        });
                        jsonResult = Json(new { data = sensorHubItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "WaterLevelSensor":
                        var waterLevelSensorItems = this.applicationService.AppWaterLevelSensorItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            DevEUI = m.DevEUI,
                            GatewayEUI = m.GatewayEUI,
                            WaterLevel = m.WaterLevel,
                            Distance = m.Distance,
                            BATLevel = m.BATLevel,
                            BATVolt = m.BATVolt,
                            RSSI = m.RSSI,
                            SNR = m.SNR
                        });
                        jsonResult = Json(new { data = waterLevelSensorItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "WaterQualitySensor":
                        var waterQualitySensorItems = this.applicationService.AppWaterQualitySensorItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            DevEUI = m.DevEUI,
                            GatewayEUI = m.GatewayEUI,
                            WaterTemp = m.WaterTemp,
                            DO = m.DO,
                            DO_Cal = m.DO_Cal,
                            BATLevel = m.BATLevel,
                            BATVolt = m.BATVolt,
                            RSSI = m.RSSI,
                            SNR = m.SNR
                        });
                        jsonResult = Json(new { data = waterQualitySensorItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "WasteBinSensor":
                        var wasteBinSensorItems = this.applicationService.AppWasteBinSensorItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            DevEUI = m.DevEUI,
                            GatewayEUI = m.GatewayEUI,
                            Full = m.Full,
                            Flame = m.Flame,
                            AirLevel = m.AirLevel,
                            Battery = m.Battery,
                            RSSI = m.RSSI,
                            SNR = m.SNR
                        });
                        jsonResult = Json(new { data = wasteBinSensorItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "WeatherSensor":
                        var weatherSensorItems = this.applicationService.AppWeatherSensorItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            DevEUI = m.DevEUI,
                            GatewayEUI = m.GatewayEUI,
                            WindSpeed = m.WindSpeed,
                            WindVane = m.WindVane,
                            RainfallCurrentHour = m.RainfallCurrentHour,
                            RainfallPreviousHour = m.RainfallPreviousHour,
                            RainfallLast_24Hours = m.RainfallLast_24Hours,
                            Luminosity = m.Luminosity,
                            BATLevel = m.BATLevel,
                            BATVolt = m.BATVolt,
                            RSSI = m.RSSI,
                            SNR = m.SNR
                        });
                        jsonResult = Json(new { data = weatherSensorItems }, JsonRequestBehavior.AllowGet);
                        break;

                    case "GpsTracking":
                        var GspTrackingItems = this.applicationService.AppGspTrackingItems(filterOptions).Select(m => new
                        {
                            DateTime = m.DateTime.ToString("yyyy/MM/dd HH:mm:ss"),
                            DevEUI = m.DevEUI,
                            GatewayEUI = m.GatewayEUI,
                            Latitude = m.Latitude,
                            Longitude = m.Longitude,
                            Emergency = m.Emergency,
                            Battery = m.Battery,
                            RSSI = m.RSSI,
                            SNR = m.SNR
                        });
                        jsonResult = Json(new { data = GspTrackingItems }, JsonRequestBehavior.AllowGet);
                        break;
                }              
            }
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;
        }
    }
}