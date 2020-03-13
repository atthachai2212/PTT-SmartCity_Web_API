using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class WasteBinService : IWasteBinService
    {
        private dbSmartCityContext db;

        public WasteBinService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbWasteBinSensor> WasteBinSensorItems => this.db.tbWasteBinSensor.ToList();

        public void WasteBinSensorData(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var wasteBinSensor = this.db.tbWasteBinSensor
                    .Where(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute)
                    .FirstOrDefault();
                if(wasteBinSensor == null)
                {
                    this.WasteBinSensorDataInsert(model);
                }
                
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WasteBinSensorDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoraDataService.LAS_C01L_Sensor(model.raw_data);
            try
            {
                tbWasteBinSensor wasteBinSensor = new tbWasteBinSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Full = data.Full,
                    Flame = data.Flame,
                    AirLevel = data.AirLevel,
                    Battery = 0,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWasteBinSensor.Add(wasteBinSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}