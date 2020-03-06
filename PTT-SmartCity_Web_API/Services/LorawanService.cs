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
    public class LoraWANService : ILoraWANService
    {
        //private dbSmartCityContext db = new dbSmartCityContext();
        private dbSmartCityContext db;

        public LoraWANService()
        {
            db = new dbSmartCityContext();
        }

        public IEnumerable<tbLoRaWAN> LorawanItems => this.db.tbLoRaWAN.ToList();

        public IEnumerable<tbLoRaWAN_RealTime> LorawanRealtimeItems => this.db.tbLoRaWAN_RealTime.ToList();

        public void LorawanData(LorawanServiceModel model)
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

        public void LorawanDataInsert(LorawanServiceModel model)
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

        public void LorawanRealtimeDataInsert(LorawanServiceModel model)
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

        public void LorawanRealtimeDataUpdate(LorawanServiceModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var lorawanDataUpdate = this.LorawanRealtimeItems.SingleOrDefault(l => l.DevEUI == model.deveui && l.GatewayEUI == model.gateway_eui);
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