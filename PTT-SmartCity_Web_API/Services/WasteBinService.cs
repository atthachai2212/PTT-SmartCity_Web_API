using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class WasteBinService : IWasteBinService
    {
        private dbLoRaSmartCityContext db;

        public WasteBinService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public List<GetWasteBinData> wasteBinSensorItemsFilter(int yearDb_start, int yearDb_end)
        {
            var wasteBinSensorItems = new List<GetWasteBinData>();
            for (int year = yearDb_start; year <= yearDb_end; year++)
            {
                using (dbLoRaSmartCityContext context = new dbLoRaSmartCityContext(year))
                {
                    var modelWasteBin = context.tbWasteBinSensor.Select(m => new GetWasteBinData
                    {
                        Date = m.Date,
                        Time = m.Time,
                        DevEUI = m.DevEUI,
                        GatewayEUI = m.GatewayEUI,
                        Full = m.Full,
                        Flame = m.Flame,
                        AirLevel = m.AirLevel,
                        Battery = m.Battery,
                        RSSI = m.RSSI,
                        SNR = m.SNR
                    }).ToList();
                    wasteBinSensorItems.AddRange(modelWasteBin);
                }
            }
            return wasteBinSensorItems;
        }

        public IEnumerable<GetWasteBinData> wasteBinSensorItems => this.db.tbWasteBinSensor.Select(m => new GetWasteBinData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            Full = m.Full,
            Flame = m.Flame,
            AirLevel = m.AirLevel,
            Battery = m.Battery,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetWasteBinData> getWasteBinSensor => this.wasteBinSensorItemsFilter(DateTime.Now.Year - 1,DateTime.Now.Year)
            .OrderByDescending(x => x.Date).ThenByDescending(x => x.Time)
            .GroupBy(m => m.DevEUI, (key, g) => new GetWasteBinData
            {
                Date = g.FirstOrDefault().Date,
                Time = g.FirstOrDefault().Time,
                DevEUI = key,
                GatewayEUI = g.FirstOrDefault().GatewayEUI,
                Full = g.FirstOrDefault().Full,
                Flame = g.FirstOrDefault().Flame,
                AirLevel = g.FirstOrDefault().AirLevel,
                Battery = g.FirstOrDefault().Battery,
                RSSI = g.FirstOrDefault().RSSI,
                SNR = g.FirstOrDefault().SNR
            });

        public GetWasteBinDataModel getWasteBinSensorItems()
        {
            var wasteBinSensorItems = new GetWasteBinDataModel
            {
                items = this.getWasteBinSensor.ToArray(),
                totalItems = this.getWasteBinSensor.Count()
            };
            return wasteBinSensorItems;
        }

        public GetWasteBinDataModel getWasteBinSensorItemsAll()
        {
            var wasteBinSensorItems = new GetWasteBinDataModel
            {
                items = this.wasteBinSensorItems.ToArray(),
                totalItems = this.wasteBinSensorItems.Count()
            };
            return wasteBinSensorItems;
        }

        public GetWasteBinDataModel getWasteBinSensorItemsFilter(WasteBinDataFilterOptions filters)
        {
            DateTime startDt = Convert.ToDateTime(filters.startDate);
            DateTime limetDt = Convert.ToDateTime(filters.limitDate);

            GetWasteBinDataModel wasteBinSensorItems = new GetWasteBinDataModel();
            if (filters != null)
            {
                wasteBinSensorItems = new GetWasteBinDataModel
                {
                    items = this.wasteBinSensorItemsFilter(2020, DateTime.Now.Year).Take(filters.length).ToArray(),
                    totalItems = filters.length
                };

                if (!string.IsNullOrEmpty(filters.deveui) && filters.startDate != null)
                {
                    IEnumerable<GetWasteBinData> searchItem = new GetWasteBinData[] { };
                    if (filters.limitDate != null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).ToList();
                        }
                    }
                    wasteBinSensorItems.items = searchItem.ToArray();
                    wasteBinSensorItems.totalItems = searchItem.Count();
                }
                else if (!string.IsNullOrEmpty(filters.deveui))
                {
                    IEnumerable<GetWasteBinData> searchItem = new GetWasteBinData[] { };

                    if (filters.length > 0)
                    {
                        searchItem = this.wasteBinSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                    }
                    else
                    {
                        var lastDate = this.wasteBinSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).FirstOrDefault().Date;
                        searchItem = this.wasteBinSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date == lastDate).ToList();
                    }
                    wasteBinSensorItems.items = searchItem.ToArray();
                    wasteBinSensorItems.totalItems = searchItem.Count();
                }
                else if (filters.startDate != null)
                {
                    IEnumerable<GetWasteBinData> searchItem = new GetWasteBinData[] { };
                    if (filters.limitDate == null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.wasteBinSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    wasteBinSensorItems.items = searchItem.ToArray();
                    wasteBinSensorItems.totalItems = searchItem.Count();
                }
                else if (filters.limitDate != null)
                {
                    IEnumerable<GetWasteBinData> searchItem = new GetWasteBinData[] { };
                    if (filters.length > 0)
                    {
                        searchItem = this.wasteBinSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.Date <= filters.limitDate).Take(filters.length).ToList();
                    }
                    else
                    {
                        searchItem = this.wasteBinSensorItemsFilter(2020, limetDt.Year).Where(x => x.Date == filters.limitDate).ToList();
                    }
                    wasteBinSensorItems.items = searchItem.ToArray();
                    wasteBinSensorItems.totalItems = searchItem.Count();
                }
            }
            return wasteBinSensorItems;
        }

        public void WasteBinSensorData(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var wasteBinSensor = this.db.tbWasteBinSensor
                    .Where(x => x.DevEUI == model.deveui && x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute)
                    .FirstOrDefault();
                if(wasteBinSensor == null)
                {
                    this.WasteBinSensorDataInsert(model);
                }
                
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WasteBinSensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.LAS_C01L_Sensor(model.raw_data);
            try
            {
                tbWasteBinSensor wasteBinSensor = new tbWasteBinSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    Full = data.Full,
                    Flame = data.Flame,
                    AirLevel = data.AirLevel,
                    Battery = 0,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWasteBinSensor.Add(wasteBinSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}