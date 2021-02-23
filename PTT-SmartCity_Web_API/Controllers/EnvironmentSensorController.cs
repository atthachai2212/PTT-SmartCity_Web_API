using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class EnvironmentSensorController : ApiController
    {
        private IEnvironmentService environmentService;

        public EnvironmentSensorController()
        {
            this.environmentService = new EnvironmentService();
        }

        // GET: api/environmentsensor
        [Route("api/environmentsensor")]
        public GetEnvironmentDataModel GetEnvironmentSensor()
        {

            return this.environmentService.getEnvironmentSensorItems();
        }

        // GET: api/environmentsensor/all
        //[Route("api/environmentsensor/all")]
        //public GetEnvironmentDataModel GetWasteBinSensorAll()
        //{
        //    return this.environmentService.getEnvironmentSensorItemsAll();
        //}

        // GET: api/wastebinsensor/filter
        [Route("api/environmentsensor/filter")]
        public GetEnvironmentDataModel GetEnvironmentSensorFilter([FromUri]EnvironmentDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.environmentService.getEnvironmentSensorItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }

    }
}