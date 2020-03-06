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

        public LoRaWANController()
        {
            this.lorawanService = new LoraWANService();
            this.wasteBinService = new WasteBinService();
        }

        // POST: api/LoRaWAN
        [Route("api/lorawan")]
        [ResponseType(typeof(LorawanServiceModel))]
        public IHttpActionResult PostLoRaWAN(LorawanServiceModel model)
        {
            if (ModelState.IsValid)
            {
                string devAddr = model.devaddr;
                try
                {
                    this.lorawanService.LorawanData(model);
                    switch (devAddr)
                    {
                        case "0028dcb6":
                            this.wasteBinService.WasteBinSensorData(model);
                            break;
                        case "fe05168f":
                            break;
                        default:
                            break;
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