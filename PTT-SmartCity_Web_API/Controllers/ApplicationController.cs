using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
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

        // GET: Application
        public async Task <ActionResult> EnvironmentSensor()
        {
            using (var context = new dbSmartCityContext())
            {
                var model = await context.tbEnvironmentSensor.ToListAsync();
                return View(model);
            }
        }
        public ActionResult GpsTracking()
        {
            var model = this.applicationService.GspTrackingItems;
            return View(model);
        }
        public ActionResult SensorHub()
        {
            var model = this.applicationService.sensorHubItems;
            return View(model);
        }
        public ActionResult WasteBinSensor()
        {
            var model = this.applicationService.wasteBinSensorItems;
            return View(model);
        }
        public ActionResult WaterLevelSensor()
        {
            var model = this.applicationService.waterLevelSensorItems;
            return View(model);
        }
        public ActionResult WaterQualitySensor()
        {
            var model = this.applicationService.waterQualitySensorItems;
            return View(model);
        }
        public ActionResult WeatherSensor()
        {
            var model = this.applicationService.weatherSensorItems;
            return View(model);
        }
    }
}