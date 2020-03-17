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

        public IEnumerable<tbWaterLevelSensor> WaterLevelSensorItems => this.db.tbWaterLevelSensor.ToList();

        public IEnumerable<tbWaterQualitySensor> WaterQualitySensorItems => this.db.tbWaterQualitySensor.ToList();

        public void WaterLevelSensorData(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var waterLevelSensor = this.db.tbWaterLevelSensor
                    .Where(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute)
                    .FirstOrDefault();
                if (waterLevelSensor == null)
                {
                    this.WaterLevelSensorDataInsert(model);
                }

            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WaterLevelSensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.SSB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbWaterLevelSensor waterSensor = new tbWaterLevelSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    WaterLevel = data.LEV_Cal,
                    Distance = data.USdist,
                    BATVolt = data.BATVolt / 1000f,
                    BATLevel = data.BAT,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWaterLevelSensor.Add(waterSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WaterQualitySensorData(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var waterQualitySensor = this.db.tbWaterQualitySensor
                    .SingleOrDefault(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute);
                if (waterQualitySensor == null)
                {
                    this.WaterQualitySensorDataInsert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WaterQualitySensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.SWB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbWaterQualitySensor waterQualitySensor = new tbWaterQualitySensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    WaterTemp = data.WT / 100f,
                    DO = data.DO / 100f,
                    DO_Cal = data.DO_Cal / 1000f,
                    BATVolt = data.BATVolt / 1000f,
                    BATLevel = data.BAT,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWaterQualitySensor.Add(waterQualitySensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

    }
}