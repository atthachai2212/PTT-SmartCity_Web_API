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
    public class LoRaDeviceDetailController : ApiController
    {
        private readonly ILoRaDeviceDetailService loraDeviceDetailService;

        public LoRaDeviceDetailController()
        {
            this.loraDeviceDetailService = new LoRaDeviceDetailService();
        }

        // POST: api/device/detail
        [Route("api/device/detail")]
        [ResponseType(typeof(LoRaDeviceDetailDataModel))]
        public IHttpActionResult PostLoRaDeviceDetail(LoRaDeviceDetailDataModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.loraDeviceDetailService.LoRaDeviceDataInsert(model);
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
