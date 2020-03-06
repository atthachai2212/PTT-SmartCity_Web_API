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

        public void SensorHubData(LorawanServiceModel model)
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

        public void SensorHubDataInsert(LorawanServiceModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbSensorHub sensorHubData = new tbSensorHub()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Humidity = 0,
                    Temperature = 0,
                    CO2 = 0,
                    Battery = 0,
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