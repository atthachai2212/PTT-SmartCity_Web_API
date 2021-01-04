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
    public class AqiController : ApiController
    {
        private IAqiDataService aqiDataService;
        public AqiController()
        {
            this.aqiDataService = new AqiDataService();
        }

        [HttpGet]
        [Route("api/aqi")]
        public IEnumerable<GetAQIDataModel> GetAQIData()
        {
            List<GetAQIDataModel> lstGetAQIData = new List<GetAQIDataModel>();

            lstGetAQIData.Add(this.aqiDataService.getAQIAllZone());
            lstGetAQIData.Add(this.aqiDataService.getAQICommunityZone());
            lstGetAQIData.Add(this.aqiDataService.getAQIEducationZone());
            lstGetAQIData.Add(this.aqiDataService.getAQIInnovationZone());
            return lstGetAQIData;
        }
    }
}
