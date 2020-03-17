using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class WeatherService : IWeatherService
    {
        private dbSmartCityContext db;

        public WeatherService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbWeatherSensor> WeatherSensorItems => this.db.tbWeatherSensor.ToList();

        public void WeatherSensorData(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var weatherSensor = this.db.tbWeatherSensor
                    .SingleOrDefault(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute);
                if (weatherSensor == null)
                {
                    this.WeatherSensorDataInsert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WeatherSensorDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoraDataService.SAPB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbWeatherSensor weatherSensor = new tbWeatherSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    WindSpeed = data.ANE / 100f,
                    WindVane = data.WV / 10f,
                    RainfallCurrentHour = data.PLV1 / 100f,
                    RainfallPreviousHour = data.PLV2 / 100f,
                    RainfallLast_24Hours = data.PLV3 / 10f,
                    Luminosity = data.LUX,
                    BATVolt = data.BATVolt / 1000f,
                    BATLevel = data.BAT,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWeatherSensor.Add(weatherSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}