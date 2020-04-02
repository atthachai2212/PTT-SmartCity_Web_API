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

        public void environmentSensorData(LoRaWANDataModel model)
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

        public void environmentSensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.SEPB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbEnvironmentSensor environmentSensorData = new tbEnvironmentSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    O2 = data.O2conc / 100f,
                    O3 = data.O3conc,
                    PM1 = data.PM1 / 10f,
                    PM2_5 = data.PM2_5 / 10f,
                    PM10 = data.PM10 / 10f,
                    AirTemp = data.TC / 100f,
                    AirHumidity = data.HUM / 100f,
                    AirPressure = data.PRES / 100f,
                    BATLevel = data.BAT,
                    BATVolt = data.BATVolt / 1000f,
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