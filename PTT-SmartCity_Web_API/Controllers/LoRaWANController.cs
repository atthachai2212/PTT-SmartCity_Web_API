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
        private ILoRaWANService lorawanService;
        private IWasteBinService wasteBinService;
        private IGpsTrackingService gpsTrackingService;
        private ISensorHubService sensorHubService;
        private IWaterService waterService;
        private ILoRaDeviceSettingService loraDeviceSettingService;
        private IWeatherService weatherService;
        private IEnvironmentService environmentService;

        public LoRaWANController()
        {
            this.lorawanService = new LoRaWANService();
            this.wasteBinService = new WasteBinService();
            this.gpsTrackingService = new GpsTrackingService();
            this.sensorHubService = new SensorHubService();
            this.loraDeviceSettingService = new LoRaDeviceSettingService();
            this.waterService = new WaterService();
            this.weatherService = new WeatherService();
            this.environmentService = new EnvironmentService();
        }

        // POST: api/LoRaWAN
        [Route("api/lorawan")]
        [ResponseType(typeof(LoRaWANDataModel))]
        public IHttpActionResult PostLoRaWAN(LoRaWANDataModel model)
        {
            if (ModelState.IsValid)
            {
                if (model != null && !string.IsNullOrEmpty(model.deveui))
                {
                    var deviceType = this.loraDeviceSettingService.getDeviceType(model.deveui);
                    try
                    {
                        this.lorawanService.LorawanData(model);
                        if (deviceType.Equals(AppSettingService.GpsTracking))
                        {
                            this.gpsTrackingService.GpsData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.EnvironmentSensor))
                        {
                            this.environmentService.environmentSensorData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.SensorHub))
                        {
                            this.sensorHubService.SensorHubData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.WasteBinSensor))
                        {
                            this.wasteBinService.WasteBinSensorData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.WaterLevelSensor))
                        {
                            this.waterService.WaterLevelSensorData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.WaterQualitySensor))
                        {
                            this.waterService.WaterQualitySensorData(model);
                        }
                        else if (deviceType.Equals(AppSettingService.WeatherSensor))
                        {
                            this.weatherService.WeatherSensorData(model);
                        }

                        if (deviceType != "Unkhown")
                        {
                            return Ok("Successful.");
                        }
                        else
                        {
                            return Ok("Unkhown Device Type.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("Exception", ex.Message);
                    }

                }
                else
                {
                    ModelState.AddModelError("Exception", "Model IsNullOrEmpty");
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }

        // GET: api/LoRaWANLastData
        [Route("api/lorawandata")]
        [ResponseType(typeof(GetLoRaWANDataModel))]
        public GetLoRaWANDataModel GetLoRaWANLastData()
        {
            if (ModelState.IsValid)
            {
                return this.lorawanService.GetLoRaWANRealTimeData();
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }


        // GET: api/LoRaWANData
        [Route("api/lorawandata/filter")]
        [ResponseType(typeof(GetLoRaWANDataModel))]
        public GetLoRaWANDataModel GetLoRaWAN([FromUri]LoRaWANDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.lorawanService.GetLoRaWANData(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }


    }
}