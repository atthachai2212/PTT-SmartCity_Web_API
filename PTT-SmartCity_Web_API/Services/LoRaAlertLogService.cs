using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class LoRaAlertLogService : ILoRaAlertLogService
    {
        private readonly dbLoRaDeviceSettingContext db;
        public LoRaAlertLogService()
        {
            this.db = new dbLoRaDeviceSettingContext();
        }
        public void LoRaAlertDataInsert(LoRaAlertLogDataModel LoRaAlertLogData)
        {
            try
            {
                var items = LoRaAlertLogData;
                var strDate =  string.IsNullOrEmpty(LoRaAlertLogData.date) ? DateTime.Now.ToString() : LoRaAlertLogData.date;
                DateTime date = Convert.ToDateTime(strDate);
                tbLoRaAlertLog loraAlertLog = new tbLoRaAlertLog
                {
                    Date = date.Date,
                    Time = date.TimeOfDay,
                    DevEUI = items.device_eui,
                    AlertLevel = items.alertlevel,
                    Details = items.details
                };
                this.db.tbLoRaAlertLog.Add(loraAlertLog);
                this.db.SaveChanges();

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}