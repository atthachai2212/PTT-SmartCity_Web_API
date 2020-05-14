using PTT_SmartCity_Web_API.DataSet;
using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class LoRaDeviceSettingService : ILoRaDeviceSettingService
    {
        private dbLoRaDeviceSettingContext db;

        public LoRaDeviceSettingService()
        {
            db = new dbLoRaDeviceSettingContext();
        }

        public List<GetLoRaDeviceData> loraDeviceItems => this.db.tbLoRaDevice.Select(m => new GetLoRaDeviceData
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
            Created = m.Created,
            Updated = m.Updated
         }).OrderByDescending(m => m.Updated).ToList();

        public List<GetLoRaDeviceListData> loraDeviceListItems => this.db.tbLoRaDeviceList.Select(m => new GetLoRaDeviceListData
        {
            DevEUI = m.DevEUI,
            DevAddr = m.DevAddr,
            DevType = m.DevType,
            DevModel = m.DevModel,
            GatewayEUI = m.GatewayEUI,
            Activate = m.Activate,
            Class = m.Class,
            Channel = m.Channel,
            AppSKey = m.AppSKey,
            NwkSkey = m.NwkSkey,
            Created = m.Created,
            Updated = m.Updated
        }).OrderByDescending(m => m.Updated).ToList();

        public List<GetLoRaGatewayData> loraGatewayItems => this.db.tbLoRaGateway.Select(m => new GetLoRaGatewayData {
            GatewayEUI = m.GatewayEUI,
            IP_Address = m.IP_Address,
            Port = m.Port,
            API_Token = m.API_Token,
            Created = m.Created,
            Updated = m.Updated
        }).OrderByDescending(m => m.Updated).ToList();

        public string getDeviceType(string DevEUI)
        {
            var devType = this.db.tbLoRaDevice.SingleOrDefault(s => s.DevEUI.Equals(DevEUI))?.DevType ?? "Unkhown";
            return devType;
        }

        public List<string> loraDeviceTypeItems()
        {
            var ds = new dsDeviceType();
            string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/DataSet"), "Devices.xml");
            if (File.Exists(path))
            {
                ds.ReadXml(path);
            };

            var devTypeModel = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow.Field<string>("TypeName")).ToList();
            return devTypeModel;
        }

        public void LoRaDeviceCreate(GetLoRaDeviceData loraDeviceData)
        {
            var deviceData = this.loraDeviceItems
                .SingleOrDefault(m => m.DevEUI == loraDeviceData.DevEUI);

            if (deviceData == null)
            {
                var loraDevice = new tbLoRaDevice
                {
                    DevEUI = loraDeviceData.DevEUI,
                    DevAddr = loraDeviceData.DevAddr,
                    DevType = loraDeviceData.DevType,
                    DevModel = loraDeviceData.DevModel,
                    Activate = loraDeviceData.Activate,
                    Class = loraDeviceData.Class,
                    Channel = loraDeviceData.Channel,
                    AppSKey = loraDeviceData.AppSKey,
                    NwkSkey = loraDeviceData.NwkSkey,
                    Created = loraDeviceData.Created,
                    Updated = loraDeviceData.Updated
                };
                this.db.tbLoRaDevice.Add(loraDevice);
                this.db.SaveChanges();
            }
        }
        public void LoRaDeviceUpdate(GetLoRaDeviceData loraDeviceData)
        {
            try
            {
                var deviceData = this.db.tbLoRaDevice
                    .SingleOrDefault(m => m.DevEUI == loraDeviceData.DevEUI);
                if (deviceData == null)
                {
                    throw new Exception("Not Found Data.");
                }
                else
                {
                    this.db.tbLoRaDevice.Attach(deviceData);
                    deviceData.DevEUI = loraDeviceData.DevEUI;
                    deviceData.DevAddr = loraDeviceData.DevAddr;
                    deviceData.DevType = loraDeviceData.DevType;
                    deviceData.DevModel = loraDeviceData.DevModel;
                    deviceData.Activate = loraDeviceData.Activate;
                    deviceData.Class = loraDeviceData.Class;
                    deviceData.Channel = loraDeviceData.Channel;
                    deviceData.AppSKey = loraDeviceData.AppSKey;
                    deviceData.NwkSkey = loraDeviceData.NwkSkey;
                    deviceData.Updated = loraDeviceData.Updated;
                    this.db.Entry(deviceData).State = EntityState.Modified;
                    this.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void LoRaDeviceDelete(string DevEUI)
        {
            try
            {
                var loraDevice = this.db.tbLoRaDevice.SingleOrDefault(m => m.DevEUI == DevEUI);
                if (loraDevice == null)
                    throw new Exception("Not Found LoRaDevice.");
                this.db.tbLoRaDevice.Remove(loraDevice);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }


        public void LoRaGatewayCreate(GetLoRaGatewayData loraGatewayData)
        {
            var gatewayData = this.loraGatewayItems
                .SingleOrDefault(m => m.GatewayEUI == loraGatewayData.GatewayEUI);

            if(gatewayData == null)
            {
                var loraGateway = new tbLoRaGateway
                {
                    GatewayEUI = loraGatewayData.GatewayEUI,
                    IP_Address = loraGatewayData.IP_Address,
                    Port = loraGatewayData.Port,
                    API_Token = loraGatewayData.API_Token,
                    Created = loraGatewayData.Created,
                    Updated = loraGatewayData.Updated
                };
                this.db.tbLoRaGateway.Add(loraGateway);
                this.db.SaveChanges();
            }

        }

        public void LoRaGatewayUpdate(GetLoRaGatewayData loraGatewayData)
        {
            try
            {
                var gatewayData = this.db.tbLoRaGateway
                    .SingleOrDefault(m => m.GatewayEUI == loraGatewayData.GatewayEUI);
                if(gatewayData == null)
                {
                    throw new Exception("Not Found LoRaGateway.");
                }
                else
                {
                    this.db.tbLoRaGateway.Attach(gatewayData);
                    gatewayData.GatewayEUI = loraGatewayData.GatewayEUI;
                    gatewayData.IP_Address = loraGatewayData.IP_Address;
                    gatewayData.Port = loraGatewayData.Port;
                    gatewayData.API_Token = loraGatewayData.API_Token;
                    gatewayData.Updated = loraGatewayData.Updated;
                    this.db.Entry(gatewayData).State = EntityState.Modified;
                    this.db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void LoRaGatewayDelete(string GatewayEUI)
        {
            try
            {
                var loraGateway = this.db.tbLoRaGateway.SingleOrDefault(m => m.GatewayEUI == GatewayEUI);
                if (loraGateway == null) 
                    throw new Exception("Not Found Gateway.");
                this.db.tbLoRaGateway.Remove(loraGateway);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }


    }
}