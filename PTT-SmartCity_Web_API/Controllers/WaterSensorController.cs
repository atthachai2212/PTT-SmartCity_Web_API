using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class WaterSensorController : ApiController
    {
        private readonly IWaterService waterService;

        public WaterSensorController()
        {
            this.waterService = new WaterService();
        }

        // GET: api/waterlevelsensor
        [Route("api/waterlevelsensor")]
        public GetWaterLevelDataModel GetWaterLevelSensor()
        {
            return this.waterService.getWaterLevelSensorItems();
        }

        // GET: api/weathersensor
        //[Route("api/waterlevelsensor/all")]
        //public GetWaterLevelDataModel GetWaterLevelSensorAll()
        //{
        //    return this.waterService.getWaterLevelSensorItemsAll();
        //}

        // GET: api/weathersensor
        [Route("api/waterlevelsensor/filter")]
        public GetWaterLevelDataModel GetWaterLevelSensorFilter([FromUri]WaterDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.waterService.getWaterLevelSensorItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }

        // GET: api/waterqualitysensor
        [Route("api/waterqualitysensor")]
        public GetWaterQualityDataModel GetWaterQualitySensor()
        {
            return this.waterService.getWaterQualitySensorItems();
        }

        // GET: api/waterqualitysensor/all
        //[Route("api/waterqualitysensor/all")]
        //public GetWaterQualityDataModel GetWaterQualitySensorAll()
        //{
        //    return this.waterService.getWaterQualitySensorItemsAll();
        //}

        // GET: api/waterqualitysensor/filter
        [Route("api/waterqualitysensor/filter")]
        public GetWaterQualityDataModel GetWaterQualitySensorFilter([FromUri]WaterDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.waterService.getWaterQualitySensorItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }
    }
}
