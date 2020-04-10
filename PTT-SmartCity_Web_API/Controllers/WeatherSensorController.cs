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
    public class WeatherSensorController : ApiController
    {
        private IWeatherService weatherService;

        public WeatherSensorController()
        {
            this.weatherService = new WeatherService();
        }

        // GET: api/weathersensor
        [Route("api/weathersensor")]
        public GetWeatherDataModel GetWeatherSensor()
        {
            return this.weatherService.getWeatherSensorItems();
        }

        // GET: api/weathersensor
        [Route("api/weathersensor/all")]
        public GetWeatherDataModel GetWeatherSensorAll()
        {
            return this.weatherService.getWeatherSensorItemsAll();
        }

        // GET: api/weathersensor
        [Route("api/weathersensor/filter")]
        public GetWeatherDataModel GetWeatherSensorFilter([FromUri]WeatherDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.weatherService.getWeatherSensorItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));  
        }

    }
}
