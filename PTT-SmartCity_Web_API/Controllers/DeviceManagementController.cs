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
            ViewBag.AppTitle = "LoRaGateway";
            ViewBag.AppTitleIcon = "fa fa-th-list";
            ViewBag.AppSubtitle = "SmartCity LoRaGateway";
            ViewBag.AppBreadcrumbMemu = "DeviceManagement";
            ViewBag.AppBreadcrumbItem = "LoRaGateway";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-tablet";
            return View();
        }

        public ActionResult LoRaDevice()
        {
            ViewBag.AppTitle = "LoRaDevice";
            ViewBag.AppTitleIcon = "fa fa-object-ungroup";
            ViewBag.AppSubtitle = "SmartCity LoRaDevice";
            ViewBag.AppBreadcrumbMemu = "DeviceManagement";
            ViewBag.AppBreadcrumbItem = "LoRaDevice";
            ViewBag.AppBreadcrumbItemIcon = "fa fa-tablet";
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

        public JsonResult LoRaDevicePost(string PostType, string DevAddr, string DevEUI, string DevType, string DevModel, string Activate, string Class, string Channel, string AppSKey, string NwkSkey)
        {
            try
            {
                if (!string.IsNullOrEmpty(PostType))
                {
                    switch (PostType.ToLower())
                    {
                        case "create":
                            var loraDeviceCreate = new GetLoRaDeviceData
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
                            this.loRaDeviceSettingService.LoRaDeviceCreate(loraDeviceCreate);
                            break;
                        case "update":
                            var loraDeviceUpdate = new GetLoRaDeviceData
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
                                Updated = DateTime.Now
                            };
                            this.loRaDeviceSettingService.LoRaDeviceUpdate(loraDeviceUpdate);
                            break;
                        default:
                            break;
                    }
                    return Json(new { message = "success",postType = PostType });
                }
                return Json(new { message = "PostType is Empty" });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.GetErrorException() });
            }      
        }

        public JsonResult DeviceUpdate(string DevEUI)
        {
            try
            {
                if (!string.IsNullOrEmpty(DevEUI))
                {
                    var loraDevice = this.loRaDeviceSettingService.loraDeviceItems.SingleOrDefault(m => m.DevEUI == DevEUI);
                    var jsonResult = Json(new { data = loraDevice }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    throw new Exception("DeviceEUI is Empty");
                }

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
        public JsonResult DeviceDelete(string DevEUI)
        {
            try
            {
                if (!string.IsNullOrEmpty(DevEUI)) {
                    this.loRaDeviceSettingService.LoRaDeviceDelete(DevEUI);
                }
                else
                {
                    throw new Exception("DeviceEUI is Empty");
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.GetErrorException() });
            }
            return Json(new { message = "success" });
        }

        public JsonResult LoRaGatewayPost(string PostType,string GatewayEUI, string IP_Address, string Port, string API_Token)
        {
            try
            {
                if (!string.IsNullOrEmpty(PostType))
                {
                    switch (PostType.ToLower())
                    {
                        case "create":
                            var loraGatewayCreate = new GetLoRaGatewayData
                            {
                                GatewayEUI = GatewayEUI,
                                IP_Address = IP_Address,
                                Port = Port,
                                API_Token = API_Token,
                                Created = DateTime.Now,
                                Updated = DateTime.Now
                            };
                            this.loRaDeviceSettingService.LoRaGatewayCreate(loraGatewayCreate);
                            break;
                        case "update":
                            var loraGatewayUpdate = new GetLoRaGatewayData
                            {
                                GatewayEUI = GatewayEUI,
                                IP_Address = IP_Address,
                                Port = Port,
                                API_Token = API_Token,
                                Updated = DateTime.Now
                            };
                            this.loRaDeviceSettingService.LoRaGatewayUpdate(loraGatewayUpdate);
                            break;
                        default:
                            break;
                    }
                    return Json(new { message = "success", postType = PostType });
                }
                return Json(new { message = "PostType is Empty" });
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.GetErrorException() });
            }
        }

        public JsonResult GatewayUpdate(string GatewayEUI)
        {
            try
            {
                if (!string.IsNullOrEmpty(GatewayEUI))
                {
                    var loraDevice = this.loRaDeviceSettingService.loraGatewayItems.SingleOrDefault(m => m.GatewayEUI == GatewayEUI);
                    var jsonResult = Json(new { data = loraDevice }, JsonRequestBehavior.AllowGet);
                    jsonResult.MaxJsonLength = int.MaxValue;
                    return jsonResult;
                }
                else
                {
                    throw new Exception("GatewayEUI is Empty");
                }

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
        public JsonResult GatewayDelete(string GatewayEUI)
        {
            try
            {
                if (!string.IsNullOrEmpty(GatewayEUI))
                {
                    this.loRaDeviceSettingService.LoRaGatewayDelete(GatewayEUI);
                }
                else
                {
                    throw new Exception("GatewayEUI is Empty");
                }
            }
            catch (Exception ex)
            {
                return Json(new { message = ex.GetErrorException() });
            }
            return Json(new { message = "success" });
        }

        [HttpGet]
        public JsonResult GetLoRaDevice()
        {
            var loraDevice = this.loRaDeviceSettingService.loraDeviceItems.Select(m => new
            {
                DevEUI = m.DevEUI,
                DevAddr = m.DevAddr,
                DevType = m.DevType,
                DevModel = m.DevModel,
                Activate = m.Activate,
                Class = m.Class,
                Channel = m.Channel,
                AppSKey = m.AppSKey,
                NwkSkey = m.NwkSkey,
                Created = m.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                Updated = m.Updated.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();
            var jsonResult = Json(new { data = loraDevice }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

        [HttpGet]
        public JsonResult GetLoRaGateway()
        {
            var loraGateway = this.loRaDeviceSettingService.loraGatewayItems.Select(m => new
            {
                GatewayEUI = m.GatewayEUI,
                IP_Address = m.IP_Address,
                Port = m.Port,
                API_Token = m.API_Token,
                Created = m.Created.ToString("yyyy-MM-dd HH:mm:ss"),
                Updated = m.Updated.ToString("yyyy-MM-dd HH:mm:ss")
            }).ToList();
            var jsonResult = Json(new { data = loraGateway }, JsonRequestBehavior.AllowGet);
            jsonResult.MaxJsonLength = int.MaxValue;
            return jsonResult;

        }

    }
}