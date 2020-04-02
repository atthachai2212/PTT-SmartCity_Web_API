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

        public void GpsData(LoRaWANDataModel model)
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

        public void GpsDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.TLM932V2_Tracker(model.raw_data);
            try
            {
                tbGPS gps = new tbGPS()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    Latitude = Convert.ToSingle(data.Latitude),
                    Longitude = Convert.ToSingle(data.Longitude),
                    Emergency = string.Empty,
                    Battery = data.Battery,
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

        public void GpsRealtimeDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.TLM932V2_Tracker(model.raw_data);
            try
            {
                tbGPS_Realtime gpsRealtime = new tbGPS_Realtime()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    Latitude = Convert.ToSingle(data.Latitude),
                    Longitude = Convert.ToSingle(data.Longitude),
                    Emergency = string.Empty,
                    Battery = data.Battery,
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
        public void GpsRealtimeDataUpdate(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.TLM932V2_Tracker(model.raw_data);
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
                gpsRealtimeUpdate.GatewayEUI = model.gateway_eui;
                gpsRealtimeUpdate.Latitude = Convert.ToSingle(data.Latitude);
                gpsRealtimeUpdate.Longitude = Convert.ToSingle(data.Longitude);
                gpsRealtimeUpdate.Emergency = string.Empty;
                gpsRealtimeUpdate.Battery = data.Battery;
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