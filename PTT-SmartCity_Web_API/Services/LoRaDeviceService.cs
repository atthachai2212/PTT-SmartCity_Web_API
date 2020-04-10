using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class LoRaDeviceService : ILoRaDeviceService
    {
        private dbSmartCityContext db;

        public LoRaDeviceService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbLoRaDevice> loraDeviceItems => this.db.tbLoRaDevice.ToList();

        public IEnumerable<string> loraDeviceTypeItems => this.db.tbLoRaDevice.GroupBy(g => g.DevType).Select(s => s.Key).ToList();

        public void CreateDevice()
        {
            throw new NotImplementedException();
        }

        public void DeleteDevice()
        {
            throw new NotImplementedException();
        }

        public string DeviceType(string DevEUI)
        {
            var devType = this.db.tbLoRaDevice.SingleOrDefault(s => s.DevEUI.Equals(DevEUI)).DevType;
            return devType;
        }

        public GetLoRaWANDeviceModel GetLoRaWANDevice(LoRaWANDeviceFilterOptions filters)
        {
            var items = this.loraDeviceItems.Select(m => new GetLoRaWANDevice
            {
                DevEUI = m.DevEUI,
                DevModel = m.DevModel,
                GatewayEUI = m.GatewayEUI,
                DevType = m.DevType,
                Created = m.Created,
                Updated = m.Updated
            }).OrderByDescending(m => m.Updated);

            var loraDeviceItems = new GetLoRaWANDeviceModel
            {
                items = items.ToArray(),
                totalItems = items.Count()
            };
            return loraDeviceItems;
        }

        public void UpdateDevice()
        {
            throw new NotImplementedException();
        }

    }
}