using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class SensorHubService : ISensorHubService
    {
        private dbSmartCityContext db;

        public SensorHubService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbSensorHub> sensorHubItems => this.db.tbSensorHub.ToList();

        public void SensorHubData(LoraWANDataModel model)
        {
            try
            {
                if(model != null)
                {
                    this.SensorHubDataInsert(model);
                }
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }

        public void SensorHubDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoraDataService.LAS_501L_Sensor(model.raw_data);
            try
            {
                tbSensorHub sensorHubData = new tbSensorHub()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Humidity = data.Humidity,
                    Temperature = data.Temperature,
                    CO2 = data.CO2,
                    BatteryVolt = data.BatteryVolt,
                    BatteryCurrent = data.BatteryCurrent,
                    BatteryPercent = data.BatteryPercent,
                    BatteryTemp = data.BatteryTemp,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbSensorHub.Add(sensorHubData);
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }
    }
}