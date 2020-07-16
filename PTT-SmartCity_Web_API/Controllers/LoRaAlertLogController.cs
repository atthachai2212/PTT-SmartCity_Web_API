using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class LoRaAlertLogController : ApiController
    {
        private readonly ILoRaAlertLogService loraAlertLogService;

        public LoRaAlertLogController()
        {
            this.loraAlertLogService = new LoRaAlertLogService();
        }
        // POST: api/alert
        [Route("api/alertlog")]
        //[ResponseType(typeof(LoRaAlertLogDataModel))]
        public IHttpActionResult PostLoRaAlert(LoRaAlertLogDataModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.loraAlertLogService.LoRaAlertDataInsert(model);
                    return Ok("Successful.");
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return BadRequest(ModelState.GetErrorModelState());
        }
    }
}
