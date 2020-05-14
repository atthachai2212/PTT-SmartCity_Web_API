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
            ViewBag.AppTitle = "Dashboard";
            ViewBag.AppTitleIcon = "fa fa-wifi";
            ViewBag.AppSubtitle = "SmartCity Web API";
            ViewBag.AppBreadcrumbMemu = "Dashboard";
            ViewBag.AppBreadcrumbItem = "Home";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-home";
            ViewBag.devTypeConut = this.loRaDeviceSettingService.loraDeviceItems;
            ViewBag.gwConut = this.loRaDeviceSettingService.loraGatewayItems;
            return View();
        }

        public ActionResult UplinkData()
        {
            ViewBag.AppTitle = "UplinkData";
            ViewBag.AppTitleIcon = "fa fa-database";
            ViewBag.AppSubtitle = "SmartCity LoRaWAN UplinkData";
            ViewBag.AppBreadcrumbMemu = "LoRaWAN";
            ViewBag.AppBreadcrumbItem = "UplinkData";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-wifi";
            //var model = this.loRaWANService.GetLoRaWANRealTimeData();
            ViewBag.DevEUI = this.loRaWANService.LorawanRealtimeItems.GroupBy(g => g.DevEUI).Select(s => s.Key).ToList();
            ViewBag.GatewayEUI = this.loRaWANService.LorawanRealtimeItems.GroupBy(g => g.GatewayEUI).Select(s => s.Key).ToList();
            ViewBag.uplinkCount = this.loRaWANService.LorawanRealtimeItems.Count();
            return View();
        }

        [HttpGet]
        public JsonResult GetUplinkData(string EUIFilter, string GatewayFilter)
        {
            var lorawanRealtimeItems = new List<GetLoRaWANData>();
            if (!string.IsNullOrEmpty(EUIFilter) && !string.IsNullOrEmpty(GatewayFilter))
            {
                lorawanRealtimeItems = this.loRaWANService.LorawanRealtimeItems.Where(m => m.DevEUI == EUIFilter && m.GatewayEUI == GatewayFilter).ToList();
            }
            else if (!string.IsNullOrEmpty(GatewayFilter))
            {
                lorawanRealtimeItems = this.loRaWANService.LorawanRealtimeItems.Where(m => m.GatewayEUI == GatewayFilter).ToList();
            }
            else if(!string.IsNullOrEmpty(EUIFilter))
            {
                lorawanRealtimeItems = this.loRaWANService.LorawanRealtimeItems.Where(m => m.DevEUI == EUIFilter).ToList();
            }
            else
            {
                lorawanRealtimeItems = this.loRaWANService.LorawanRealtimeItems.ToList();
            }

            var items = lorawanRealtimeItems.Select(m => new {
                DateTime = string.Format($"{m.Date.ToString("yyyy/MM/dd")}  {m.Time.ToString("hh\\:mm\\:ss")}"),
                DevAddr = m.DevAddr,
                DevEUI = m.DevEUI,
                DevType = this.loRaDeviceSettingService.getDeviceType(m.DevEUI),
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
            ViewBag.AppTitle = "DeviceList";
            ViewBag.AppTitleIcon = "fa fa-gears";
            ViewBag.AppSubtitle = "SmartCity DeviceList";
            ViewBag.AppBreadcrumbMemu = "LoRaWAN";
            ViewBag.AppBreadcrumbItem = "DeviceList";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-wifi";
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
