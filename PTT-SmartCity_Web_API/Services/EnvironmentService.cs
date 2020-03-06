using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private dbSmartCityContext db;

        public EnvironmentService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbEnvironmentSensor> environmentSensorItems => this.db.tbEnvironmentSensor.ToList();

        public void environmentSensorData(LorawanServiceModel model)
        {
            try
            {
                if(model != null)
                {
                    this.environmentSensorDataInsert(model);
                }
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }

        public void environmentSensorDataInsert(LorawanServiceModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbEnvironmentSensor environmentSensorData = new tbEnvironmentSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    O2 = 0,
                    O3 = 0,
                    PM1 = 0,
                    PM2_5 = 0,
                    PM10 = 0,
                    Battery = 0,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbEnvironmentSensor.Add(environmentSensorData);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }
    }
}