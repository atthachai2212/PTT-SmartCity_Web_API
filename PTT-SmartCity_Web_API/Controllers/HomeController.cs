using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using PTT_SmartCity_Web_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using PTT_SmartCity_Web_API.DataSet;
using System.Data;

namespace PTT_SmartCity_Web_API.Controllers
{
    public class HomeController : Controller
    {
        private ILoRaDeviceSettingService loRaDeviceSettingService;
        private ILoRaWANService loRaWANService;

        public HomeController()
        {
            this.loRaDeviceSettingService = new LoRaDeviceSettingService();
            this.loRaWANService = new LoRaWANService();
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";
            ViewBag.appTitle = "Dashboard";
            ViewBag.appSubtitle = "PTT SmartCity Web API";
            ViewBag.devTypeConut = this.loRaDeviceSettingService.loraDeviceItems;
            ViewBag.gwConut = this.loRaDeviceSettingService.loraGatewayItems;
            return View();
        }

        public ActionResult UplinkData()
        {
            var model = this.loRaWANService.GetLoRaWANRealTimeData();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetUplinkData()
        {
            var items = this.loRaWANService.LorawanRealtimeItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
                DevAddr = m.DevAddr,
                DevEUI = m.DevEUI,
                GatewayEUI = m.GatewayEUI,     
                RSSI = m.RSSI,
                SNR = m.SNR,
                SF = m.SF,
                BW = m.BW,
                Freq = m.Freq,
                UpCtr = m.UpCtr,
                Size = m.Size,
                Data = m.Data
            });
            return Json(new { data = items }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeviceList()
        {
            return View();
        }

        public ActionResult CreateDevice()
        {
            var ds = new dsDeviceType();
            string path = Path.Combine(Server.MapPath("~/DataSet"), "Devices.xml");
            if (System.IO.File.Exists(path))
            {
                ds.ReadXml(path);
            };
            var devTypeModel = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow.Field<string>("TypeName")).ToList();
            ViewBag.loraDeviceType = this.loRaDeviceSettingService.loraDeviceTypeItems();
            return View(devTypeModel);
        }

        public ActionResult LoRaGateway()
        {
            return View();
        }
    }
}
