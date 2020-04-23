﻿using PTT_SmartCity_Web_API.Entity;
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
        private dbLoRaSmartCityContext db;

        public WaterService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public IEnumerable<GetWaterLevelData> WaterLevelSensorItems => this.db.tbWaterLevelSensor.Select(m => new GetWaterLevelData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            WaterLevel = m.WaterLevel,
            Distance = m.Distance,
            BATVolt = m.BATVolt,
            BATLevel = m.BATLevel,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetWaterQualityData> WaterQualitySensorItems => this.db.tbWaterQualitySensor.Select(m => new GetWaterQualityData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            WaterTemp = m.WaterTemp,
            DO = m.DO,
            DO_Cal = m.DO_Cal,
            BATVolt = m.BATVolt,
            BATLevel = m.BATLevel,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetWaterLevelData> getWaterLevelSensor => this.WaterLevelSensorItems.GroupBy(m => m.DevEUI, (key, g) => new GetWaterLevelData
        {
            Date = g.FirstOrDefault().Date,
            Time = g.FirstOrDefault().Time,
            DevEUI = key,
            GatewayEUI = g.FirstOrDefault().GatewayEUI,
            WaterLevel = g.FirstOrDefault().WaterLevel,
            Distance = g.FirstOrDefault().Distance,
            BATVolt = g.FirstOrDefault().BATVolt,
            BATLevel = g.FirstOrDefault().BATLevel,
            RSSI = g.FirstOrDefault().RSSI,
            SNR = g.FirstOrDefault().SNR
        });

        public IEnumerable<GetWaterQualityData> getWaterQualitySensor => this.WaterQualitySensorItems.GroupBy(m => m.DevEUI, (key, g) => new GetWaterQualityData
        {
            Date = g.FirstOrDefault().Date,
            Time = g.FirstOrDefault().Time,
            DevEUI = key,
            GatewayEUI = g.FirstOrDefault().GatewayEUI,
            WaterTemp = g.FirstOrDefault().WaterTemp,
            DO = g.FirstOrDefault().DO,
            DO_Cal = g.FirstOrDefault().DO_Cal,
            BATVolt = g.FirstOrDefault().BATVolt,
            BATLevel = g.FirstOrDefault().BATLevel,
            RSSI = g.FirstOrDefault().RSSI,
            SNR = g.FirstOrDefault().SNR
        });


        public GetWaterLevelDataModel getWaterLevelSensorItems()
        {
            var waterLevelSensorItems = new GetWaterLevelDataModel
            {
                items = this.getWaterLevelSensor.ToArray(),
                totalItems = this.getWaterLevelSensor.Count()
            };
            return waterLevelSensorItems;
        }

        public GetWaterLevelDataModel getWaterLevelSensorItemsAll()
        {
            var waterLevelSensorItems = new GetWaterLevelDataModel
            {
                items = this.WaterLevelSensorItems.ToArray(),
                totalItems = this.WaterLevelSensorItems.Count()
            };
            return waterLevelSensorItems;
        }

        public GetWaterLevelDataModel getWaterLevelSensorItemsFilter(WaterDataFilterOptions filters)
        {
            var waterLevelSensorItems = new GetWaterLevelDataModel
            {
                items = this.WaterLevelSensorItems.Take(filters.length).ToArray(),
                totalItems = filters.length
            };

            if (!string.IsNullOrEmpty(filters.deveui))
            {
                IEnumerable<GetWaterLevelData> searchItem = new GetWaterLevelData[] { };

                if (filters.length > 0)
                {
                    searchItem = this.WaterLevelSensorItems.Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                }
                else
                {
                    searchItem = this.WaterLevelSensorItems.Where(x => x.DevEUI == filters.deveui).ToList();
                }
                waterLevelSensorItems.items = searchItem.ToArray();
                waterLevelSensorItems.totalItems = searchItem.Count();
            }
            return waterLevelSensorItems;
        }

        public GetWaterQualityDataModel getWaterQualitySensorItems()
        {
            var waterQualitySensorItems = new GetWaterQualityDataModel
            {
                items = this.getWaterQualitySensor.ToArray(),
                totalItems = this.getWaterQualitySensor.Count()
            };
            return waterQualitySensorItems;
        }

        public GetWaterQualityDataModel getWaterQualitySensorItemsAll()
        {
            var waterQualitySensorItems = new GetWaterQualityDataModel
            {
                items = this.WaterQualitySensorItems.ToArray(),
                totalItems = this.WaterQualitySensorItems.Count()
            };
            return waterQualitySensorItems;
        }

        public GetWaterQualityDataModel getWaterQualitySensorItemsFilter(WaterDataFilterOptions filters)
        {
            var waterQualitySensorItems = new GetWaterQualityDataModel
            {
                items = this.WaterQualitySensorItems.Take(filters.length).ToArray(),
                totalItems = filters.length
            };

            if (!string.IsNullOrEmpty(filters.deveui))
            {
                IEnumerable<GetWaterQualityData> searchItem = new GetWaterQualityData[] { };

                if (filters.length > 0)
                {
                    searchItem = this.WaterQualitySensorItems.Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                }
                else
                {
                    searchItem = this.WaterQualitySensorItems.Where(x => x.DevEUI == filters.deveui).ToList();
                }
                waterQualitySensorItems.items = searchItem.ToArray();
                waterQualitySensorItems.totalItems = searchItem.Count();
            }
            return waterQualitySensorItems;
        }

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
                    GatewayEUI = model.gateway_eui,
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
                    GatewayEUI = model.gateway_eui,
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