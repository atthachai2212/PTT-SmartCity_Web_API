using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
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

        public string DeviceType(string DevEUI)
        {
            var devType = this.db.tbLoRaDevice.SingleOrDefault(s => s.DevEUI.Equals(DevEUI)).DevType;
            return devType;
        }
    }
}