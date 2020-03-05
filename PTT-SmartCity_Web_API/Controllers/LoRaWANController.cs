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

        public LoRaWANController()
        {
            this.lorawanService = new LorawanService();
        }

        // POST: api/LoRaWAN
        [Route("api/lorawan")]
        [ResponseType(typeof(LorawanServiceModel))]
        public IHttpActionResult PostLoRaWAN(LorawanServiceModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.lorawanService.LorawanDataRealTime(model);
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