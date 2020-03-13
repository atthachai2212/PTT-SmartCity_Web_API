using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class WaterService : IWaterService
    {
        private dbSmartCityContext db;

        public WaterService()
        {
            db = new dbSmartCityContext();
        }
        public IEnumerable<tbWaterSensor> WaterSensorItems => this.db.tbWaterSensor.ToList();

        public void WaterSensorData(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var waterSensor = this.db.tbWaterSensor
                    .Where(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute)
                    .FirstOrDefault();
                if (waterSensor == null)
                {
                    this.WaterSensorDataInsert(model);
                }

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WaterSensorDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoraDataService.SSB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbWaterSensor waterSensor = new tbWaterSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Level = data.LEV_Cal,
                    DO = 0,
                    Temperature = 0,
                    BatteryVolt = data.BATVolt / 1000,
                    BatteryPercent = data.BAT,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWaterSensor.Add(waterSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}