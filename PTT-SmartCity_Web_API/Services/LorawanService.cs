using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PTT_SmartCity_Web_API.Services
{
    public class LoRaWANService : ILoRaWANService
    {

        private dbSmartCityContext db;

        public LoRaWANService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<GetLoRaWANData> LorawanItems => this.db.tbLoRaWAN.Select(m => new GetLoRaWANData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            DevAddr = m.DevAddr,
            RSSI = m.RSSI,
            SNR = m.SNR,
            SF = m.SF,
            BW = m.BW,
            Freq = m.Freq,
            UpCtr = m.UpCtr,
            Size = m.Size,
            Data = m.Data
        }).OrderByDescending(m => m.Date).ThenByDescending(m => m.Time);

        public IEnumerable<GetLoRaWANData> LorawanRealtimeItems => this.db.tbLoRaWAN_RealTime.Select(m => new GetLoRaWANData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            DevAddr = m.DevAddr,
            RSSI = m.RSSI,
            SNR = m.SNR,
            SF = m.SF,
            BW = m.BW,
            Freq = m.Freq,
            UpCtr = m.UpCtr,
            Size = m.Size,
            Data = m.Data
        }).OrderByDescending(m => m.Date).ThenByDescending(m => m.Time);

        public GetLoRaWANDataModel GetLoRaWANData(LoRaWANDataFilterOptions filters)
        {
            var loraWanItems = new GetLoRaWANDataModel
            {
                items = this.LorawanItems.Take(filters.length).ToArray(),
                totalItems = this.LorawanItems.Count()
            };

            if (!string.IsNullOrEmpty(filters.deveui))
            {
                IEnumerable<GetLoRaWANData> searchItem = new GetLoRaWANData[] { };

                if (filters.length > 0)
                {
                    searchItem = this.LorawanItems.Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                }
                else
                {
                    searchItem = this.LorawanItems.Where(x => x.DevEUI == filters.deveui).ToList();
                }
                loraWanItems.items = searchItem.ToArray();
                loraWanItems.totalItems = searchItem.Count();
            }
            return loraWanItems;
        }

        public GetLoRaWANDataModel GetLoRaWANRealTimeData()
        {
            var loraWanItems = new GetLoRaWANDataModel
            {
                items = this.LorawanRealtimeItems.ToArray(),
                totalItems = this.LorawanRealtimeItems.Count()
            };

            return loraWanItems;
        }

        public void LorawanData(LoRaWANDataModel model)
        {
			try
			{
                var lorawanDataRealTime = this.db.tbLoRaWAN_RealTime
                    .Where(x => x.DevEUI == model.deveui && x.GatewayEUI == model.gateway_eui)
                    .FirstOrDefault();

                if(lorawanDataRealTime != null)
                {
                    this.LorawanRealtimeDataUpdate(model);
                }
                else
                {
                    this.LorawanRealtimeDataInsert(model);
                }
                this.LorawanDataInsert(model);
            }
			catch (Exception ex)
			{
				throw ex.GetErrorException();
			}
        }

        public void LorawanDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbLoRaWAN Lorawan = new tbLoRaWAN
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevAddr = model.devaddr,
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    RSSI = model.rssi,
                    SNR = model.snr,
                    SF = model.sf,
                    BW = model.bw,
                    Freq = model.freq / 1000000,
                    UpCtr = model.uplink_count,
                    Size = model.datasize,
                    Data = model.raw_data
                };
                this.db.tbLoRaWAN.Add(Lorawan);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void LorawanRealtimeDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                tbLoRaWAN_RealTime LorawanRealTime = new tbLoRaWAN_RealTime
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevAddr = model.devaddr,
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    RSSI = model.rssi,
                    SNR = model.snr,
                    SF = model.sf,
                    BW = model.bw,
                    Freq = model.freq / 1000000,
                    UpCtr = model.uplink_count,
                    Size = model.datasize,
                    Data = model.raw_data
                };
                this.db.tbLoRaWAN_RealTime.Add(LorawanRealTime);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void LorawanRealtimeDataUpdate(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var lorawanDataUpdate = this.db.tbLoRaWAN_RealTime
                    .SingleOrDefault(l => l.DevEUI == model.deveui && l.GatewayEUI == model.gateway_eui);
                if (lorawanDataUpdate == null)
                    throw new Exception("Not Found Data.");
                this.db.tbLoRaWAN_RealTime.Attach(lorawanDataUpdate);
                lorawanDataUpdate.Date = dateTime.Date;
                lorawanDataUpdate.Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second);
                lorawanDataUpdate.DevAddr = model.devaddr;
                lorawanDataUpdate.DevEUI = model.deveui;
                lorawanDataUpdate.GatewayEUI = model.gateway_eui;
                lorawanDataUpdate.RSSI = model.rssi;
                lorawanDataUpdate.SNR = model.snr;
                lorawanDataUpdate.SF = model.sf;
                lorawanDataUpdate.BW = model.bw;
                lorawanDataUpdate.Freq = model.freq / 1000000;
                lorawanDataUpdate.UpCtr = model.uplink_count;
                lorawanDataUpdate.Size = model.datasize;
                lorawanDataUpdate.Data = model.raw_data;
                this.db.Entry(lorawanDataUpdate).State = EntityState.Modified;
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}