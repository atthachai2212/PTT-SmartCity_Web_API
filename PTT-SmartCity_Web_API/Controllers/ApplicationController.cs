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

        public ApplicationController()
        {
            this.applicationService = new ApplicationService();
        }

        public ActionResult Index(string view)
        {
            return View(view);
        }
        // GET: Application
        public ActionResult EnvironmentSensor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEnvironmentSensor()
        {
            var items = this.applicationService.environmentSensorItems.Select(m => new
            {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
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
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GpsTracking()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetGpsTracking()
        {
            var items = this.applicationService.GspTrackingItems.Select(m => new
            {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                Latitude = m.Latitude,
                Longitude = m.Longitude,
                Emergency = m.Emergency,
                Battery = m.Battery,
                RSSI = m.RSSI,
                SNR = m.SNR
            });
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SensorHub()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSensorHub()
        {
            var items = this.applicationService.sensorHubItems.Select(m => new
            {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
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
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WasteBinSensor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWasteBinSensor()
        {
            var items = this.applicationService.wasteBinSensorItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                Full = m.Full,
                Flame = m.Flame,
                AirLevel = m.AirLevel,
                Battery = m.Battery,
                RSSI = m.RSSI,
                SNR = m.SNR
            });
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WaterLevelSensor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWaterLevelSensor()
        {
            var items = this.applicationService.waterLevelSensorItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,
                WaterLevel = m.WaterLevel,
                Distance = m.Distance,
                BATLevel = m.BATLevel,
                BATVolt = m.BATVolt,
                RSSI = m.RSSI,
                SNR = m.SNR
            });
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult WaterQualitySensor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWaterQualitySensor()
        {
            var items = this.applicationService.waterQualitySensorItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
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
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult WeatherSensor()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetWeatherSensor()
        {
            var items = this.applicationService.weatherSensorItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
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
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }
    }
}