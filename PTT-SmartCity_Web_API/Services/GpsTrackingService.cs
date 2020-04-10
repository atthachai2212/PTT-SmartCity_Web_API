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
        public IEnumerable<GetGpsData> gpsItems => this.db.tbGPS.Select(m => new GetGpsData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            Latitude = m.Latitude,
            Longitude = m.Longitude,
            Emergency = string.Empty,
            Battery = m.Battery,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetGpsData> gpsRealTimeItems => this.db.tbGPS_Realtime.Select(m => new GetGpsData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            Latitude = m.Latitude,
            Longitude = m.Longitude,
            Emergency = string.Empty,
            Battery = m.Battery,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);


        public GetGpsDataModel getGpsItems()
        {
            var gpsItems = new GetGpsDataModel
            {
                items = this.gpsRealTimeItems.ToArray(),
                totalItems = this.gpsRealTimeItems.Count()
            };
            return gpsItems;
        }

        public GetGpsDataModel getGpsItemsAll()
        {
            var gpsItems = new GetGpsDataModel
            {
                items = this.gpsItems.ToArray(),
                totalItems = this.gpsItems.Count()
            };
            return gpsItems;
        }

        public GetGpsDataModel getGpsItemsFilter(GpsDataFilterOptions filters)
        {
            var gpsItems = new GetGpsDataModel
            {
                items = this.gpsItems.Take(filters.length).ToArray(),
                totalItems = filters.length
            };

            if (!string.IsNullOrEmpty(filters.deveui))
            {
                IEnumerable<GetGpsData> searchItem = new GetGpsData[] { };

                if (filters.length > 0)
                {
                    searchItem = this.gpsItems.Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                }
                else
                {
                    searchItem = this.gpsItems.Where(x => x.DevEUI == filters.deveui).ToList();
                }
                gpsItems.items = searchItem.ToArray();
                gpsItems.totalItems = searchItem.Count();
            }

            return gpsItems;
        }

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
                var gpsRealTimeUpdate = this.db.tbGPS_Realtime.SingleOrDefault(g => g.DevEUI == model.deveui);
                if (gpsRealTimeUpdate == null)
                    throw new Exception("Not Found Data.");
                this.db.tbGPS_Realtime.Attach(gpsRealTimeUpdate);
                gpsRealTimeUpdate.Date = dateTime.Date;
                gpsRealTimeUpdate.Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
                gpsRealTimeUpdate.DevEUI = model.deveui;
                gpsRealTimeUpdate.GatewayEUI = model.gateway_eui;
                gpsRealTimeUpdate.Latitude = Convert.ToSingle(data.Latitude);
                gpsRealTimeUpdate.Longitude = Convert.ToSingle(data.Longitude);
                gpsRealTimeUpdate.Emergency = string.Empty;
                gpsRealTimeUpdate.Battery = data.Battery;
                gpsRealTimeUpdate.RSSI = model.rssi;
                gpsRealTimeUpdate.SNR = model.snr;
                this.db.Entry(gpsRealTimeUpdate).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}