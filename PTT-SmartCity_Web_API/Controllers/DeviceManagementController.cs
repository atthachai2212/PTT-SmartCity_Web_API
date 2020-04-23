using PTT_SmartCity_Web_API.DataSet;
using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class DeviceManagementController : Controller
    {
        private ILoRaDeviceSettingService loRaDeviceSettingService;

        public DeviceManagementController()
        {
            this.loRaDeviceSettingService = new LoRaDeviceSettingService();
        }

        //GET: DeviceManagement
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoRaGateway()
        {
            return View();
        }

        public ActionResult LoRaDevice()
        {
            var devTypeModel = this.loRaDeviceSettingService.loraDeviceTypeItems();

            List<string> channelPlan = new List<string>(new string[]
            {
                "AS923",
                "EU863-870",
                "US902-928",
                "CN779-787",
                "EU433",
                "AU915-928",
                "KR920-923",
                "IND865-867",
                "RU864-870"
            });
            List<string> activation = new List<string>(new string[]
            {
                "ABP",
                "OTAA"
            });

            ViewBag.activation = activation;
            ViewBag.channelPlan = channelPlan;
            ViewBag.loraDeviceType = devTypeModel;
            return View();
        }

        public JsonResult CreateLoRaDevice(string DevAddr, string DevEUI, string DevType, string DevModel, string Activate, string Class, string Channel, string AppSKey, string NwkSkey)
        {
            try
            {
                var loraDevice = new GetLoRaDeviceData
                {
                    DevAddr = DevAddr,
                    DevEUI = DevEUI,
                    DevType = DevType,
                    DevModel = DevModel,
                    Activate = Activate,
                    Class = Class,
                    Channel = Channel,
                    AppSKey = AppSKey,
                    NwkSkey = NwkSkey,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };
                this.loRaDeviceSettingService.CreateLoRaDevice(loraDevice);
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
            return Json(new { message = "success" });
        }

        public JsonResult CreateLoRaGateway(string GatewayEUI, string IP_Address, string Port, string API_Token)
        {
            try
            {
                var loraGateway = new GetLoRaGatewayData
                {
                    GatewayEUI = GatewayEUI,
                    IP_Address = IP_Address,
                    Port = Port,
                    API_Token = API_Token,
                    Created = DateTime.Now,
                    Updated = DateTime.Now
                };
                this.loRaDeviceSettingService.CreateLoRaGateway(loraGateway);
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
            return  Json(new { message = "success" });
        }

        [HttpGet]
        public JsonResult GetLoRaDevice()
        {
            var jsonResult = Json(new { data = this.loRaDeviceSettingService.loraDeviceItems }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult GetLoRaGateway()
        {
            var jsonResult = Json(new { data = this.loRaDeviceSettingService.loraGatewayItems }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

    }
}