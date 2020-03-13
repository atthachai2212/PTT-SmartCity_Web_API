using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class GpsTrackingService : IGpsTrackingService
    {
        private dbSmartCityContext db;

        public GpsTrackingService()
        {
            db = new dbSmartCityContext();
        }
        public IEnumerable<tbGPS> gpsItems => this.db.tbGPS.ToList();

        public IEnumerable<tbGPS_Realtime> gpsRealtimeItems => this.db.tbGPS_Realtime.ToList();

        public void GpsData(LoraWANDataModel model)
        {
            try
            {
                if(model != null)
                {
                    var gpsDataRealtime = this.db.tbGPS_Realtime
                        .Where(g => g.DevEUI == model.deveui)
                        .FirstOrDefault();

                    if(gpsDataRealtime != null)
                    {
                        this.GpsRealtimeDataUpdate(model);
                    }
                    else
                    {
                        this.GpsRealtimeDataInsert(model);
                    }
                    this.GpsDataInsert(model);
                }               
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void GpsDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbGPS gps = new tbGPS()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Latitude = 0,
                    Longitude = 0,
                    Emergency = "test",
                    Battery = 0,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbGPS.Add(gps);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void GpsRealtimeDataInsert(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbGPS_Realtime gpsRealtime = new tbGPS_Realtime()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    Latitude = 0,
                    Longitude = 0,
                    Emergency = "test",
                    Battery = 0,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbGPS_Realtime.Add(gpsRealtime);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
        public void GpsRealtimeDataUpdate(LoraWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var gpsRealtimeUpdate = this.gpsRealtimeItems
                    .SingleOrDefault(g => g.DevEUI == model.deveui);
                if (gpsRealtimeUpdate == null)
                    throw new Exception("Not Found Data.");
                this.db.tbGPS_Realtime.Attach(gpsRealtimeUpdate);
                gpsRealtimeUpdate.Date = dateTime.Date;
                gpsRealtimeUpdate.Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
                gpsRealtimeUpdate.DevEUI = model.deveui;
                gpsRealtimeUpdate.Latitude = 0;
                gpsRealtimeUpdate.Longitude = 0;
                gpsRealtimeUpdate.Emergency = "test";
                gpsRealtimeUpdate.Battery = 0;
                gpsRealtimeUpdate.RSSI = model.rssi;
                gpsRealtimeUpdate.SNR = model.snr;
                this.db.Entry(gpsRealtimeUpdate).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}