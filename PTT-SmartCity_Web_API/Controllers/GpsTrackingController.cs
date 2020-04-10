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
    public class GpsTrackingController : ApiController
    {
        private IGpsTrackingService gpsTrackingService;

        public GpsTrackingController()
        {
            this.gpsTrackingService = new GpsTrackingService();
        }

        // GET: api/gpstracking
        [Route("api/gpstracking")]
        public GetGpsDataModel GetGpsTracking()
        {
            return this.gpsTrackingService.getGpsItems();
        }

        // GET: api/gpstracking/all
        [Route("api/gpstracking/all")]
        public GetGpsDataModel GetGpsTrackingAll()
        {
            return this.gpsTrackingService.getGpsItemsAll();
        }

        // GET: api/gpstracking/filter
        [Route("api/gpstracking/filter")]
        public GetGpsDataModel GetGpsTrackingFilter([FromUri]GpsDataFilterOptions filters)
        {
            if (ModelState.IsValid)
            {
                return this.gpsTrackingService.getGpsItemsFilter(filters);
            }
            throw new HttpResponseException(Request.CreateResponse(
                HttpStatusCode.BadRequest,
                new { Message = ModelState.GetErrorModelState() }
            ));
        }
    }
}