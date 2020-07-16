using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class LoRaDeviceDetailService : ILoRaDeviceDetailService
    {
        private readonly dbLoRaDeviceSettingContext db;

        public LoRaDeviceDetailService()
        {
            this.db = new dbLoRaDeviceSettingContext();
        }
        public void LoRaDeviceDataInsert(LoRaDeviceDetailDataModel LoRaDeviceDetailData)
        {
            try
            {
                var items = LoRaDeviceDetailData;
                tbLoRaDeviceDetail loraDeviceDetail = new tbLoRaDeviceDetail
                {
                    DevEUI = items.device_eui,
                    Name  = items.name,
                    Details = items.details,
                    Latitude = items.latitude,
                    Longitude = items.longitude

                };
                this.db.tbLoRaDeviceDetail.Add(loraDeviceDetail);
                this.db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}