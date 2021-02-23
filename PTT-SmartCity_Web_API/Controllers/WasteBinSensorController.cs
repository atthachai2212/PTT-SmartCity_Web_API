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
    public class WasteBinSensorController : ApiController
    {
        private IWasteBinService wasteBinService;

        public WasteBinSensorController()
        {
            this.wasteBinService = new WasteBinService();
        }

        // GET: api/wastebinsensor
        [Route("api/wastebinsensor")]
        public GetWasteBinDataModel GetWasteBinSensor()
        {
            return this.wasteBinService.getWasteBinSensorItems();
        }

        // GET: api/wastebinsensor/all
        //[Route("api/wastebinsensor/all")]
        //public GetWasteBinDataModel GetWasteBinSensorAll()
        //{
        //    return this.wasteBinService.getWasteBinSensorItemsAll();
        //}

        // GET: api/wastebinsensor/filter
        [Route("api/wastebinsensor/filter")]
        public GetWasteBinDataModel GetWasteBinSensorFilter([FromUri]WasteBinDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.wasteBinService.getWasteBinSensorItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }

    }
}