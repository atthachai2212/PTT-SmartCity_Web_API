using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class LoRaWANController : ApiController
    {
        private ILoraWANService lorawanService;
        private IWasteBinService wasteBinService;
        private IGpsTrackingService gpsTrackingService;
        private ISensorHubService sensorHubService;
        private IWaterService waterService;
        private ILoRaDeviceService loraDeviceService;

        public LoRaWANController()
        {
            this.lorawanService = new LoraWANService();
            this.wasteBinService = new WasteBinService();
            this.gpsTrackingService = new GpsTrackingService();
            this.sensorHubService = new SensorHubService();
            this.loraDeviceService = new LoRaDeviceService();
            this.waterService = new WaterService();
        }

        // POST: api/LoRaWAN
        [Route("api/lorawan")]
        [ResponseType(typeof(LoraWANDataModel))]
        public IHttpActionResult PostLoRaWAN(LoraWANDataModel model)
        {
            if (ModelState.IsValid)
            {
                var deviceType = this.loraDeviceService.DeviceType(model.deveui);
                try
                {
                    this.lorawanService.LorawanData(model);
                    if (deviceType.Equals(AppSettingService.GpsTracking))
                    {
                        this.gpsTrackingService.GpsData(model);
                    }
                    else if (deviceType.Equals(AppSettingService.SensorHub))
                    {
                        this.sensorHubService.SensorHubData(model);
                    }
                    else if (deviceType.Equals(AppSettingService.WasteBinSensor))
                    {
                        this.wasteBinService.WasteBinSensorData(model);
                    }
                    else if (deviceType.Equals(AppSettingService.WaterSensor))
                    {
                        this.waterService.WaterSensorData(model);
                    }
                    return Ok("Successful.");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("Exception", ex.Message);
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }
    }
}