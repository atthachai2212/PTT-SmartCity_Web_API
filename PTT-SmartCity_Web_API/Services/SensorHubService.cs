using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class SensorHubService : ISensorHubService
    {
        private dbLoRaSmartCityContext db;

        public SensorHubService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public List<GetSensorHubData> sensorHubItemsFilter(int yearDb_start, int yearDb_end)
        {
            var sensorHubItems = new List<GetSensorHubData>();
            for (int year = yearDb_start; year <= yearDb_end; year++)
            {
                using (dbLoRaSmartCityContext context = new dbLoRaSmartCityContext(year))
                {
                    var modelSensorHub = context.tbSensorHub.Select(m => new GetSensorHubData
                    {
                        Date = m.Date,
                        Time = m.Time,
                        DevEUI = m.DevEUI,
                        GatewayEUI = m.GatewayEUI,
                        Humidity = m.Humidity,
                        Temperature = m.Temperature,
                        CO2 = m.CO2,
                        BATVolt = m.BATVolt,
                        BATCurrent = m.BATCurrent,
                        BATLevel = m.BATLevel,
                        BATTemp = m.BATTemp,
                        RSSI = m.RSSI,
                        SNR = m.SNR
                    }).ToList();
                    sensorHubItems.AddRange(modelSensorHub);
                }
            }
            return sensorHubItems;
        }

        public IEnumerable<GetSensorHubData> sensorHubItems => this.db.tbSensorHub.Select(m => new GetSensorHubData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            Humidity = m.Humidity,
            Temperature = m.Temperature,
            CO2 = m.CO2,
            BATVolt = m.BATVolt,
            BATCurrent = m.BATCurrent,
            BATLevel = m.BATLevel,
            BATTemp = m.BATTemp,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetSensorHubData> getSensorHub => this.sensorHubItemsFilter(DateTime.Now.Year - 1 ,DateTime.Now.Year)
            .OrderByDescending(x => x.Date).ThenByDescending(x => x.Time)
            .GroupBy(m => m.DevEUI, (key, g) => new GetSensorHubData 
            {
                Date = g.FirstOrDefault().Date,
                Time = g.FirstOrDefault().Time,
                DevEUI = key,
                GatewayEUI = g.FirstOrDefault().GatewayEUI,
                Humidity = g.FirstOrDefault().Humidity,
                Temperature = g.FirstOrDefault().Temperature,
                CO2 = g.FirstOrDefault().CO2,
                BATVolt = g.FirstOrDefault().BATVolt,
                BATCurrent = g.FirstOrDefault().BATCurrent,
                BATLevel = g.FirstOrDefault().BATLevel,
                BATTemp = g.FirstOrDefault().BATTemp,
                RSSI = g.FirstOrDefault().RSSI,
                SNR = g.FirstOrDefault().SNR
            });


        public GetSensorHubDataModel getSensorHubItems()
        {
            List<GetSensorHubData> sensorHubData = new List<GetSensorHubData>();
            sensorHubData.Add(this.getSensorHub.SingleOrDefault(x => x.DevEUI == "000b78fffe05168f"));
            sensorHubData.Add(this.getSensorHub.SingleOrDefault(x => x.DevEUI == "000b78fffe05173e"));
            sensorHubData.Add(this.getSensorHub.SingleOrDefault(x => x.DevEUI == "000b78fffe051545"));
            sensorHubData.Add(this.getSensorHub.SingleOrDefault(x => x.DevEUI == "000b78fffe051742"));
            sensorHubData.Add(this.getSensorHub.SingleOrDefault(x => x.DevEUI == "000b78fffe051747"));
            var sensorHubItems = new GetSensorHubDataModel
            {
                items = sensorHubData.ToArray(),
                totalItems = sensorHubData.Count()
            };

            //var sensorHubItems = new GetSensorHubDataModel
            //{
            //    items = this.getSensorHub.ToArray(),
            //    totalItems = this.getSensorHub.Count()
            //};
            return sensorHubItems;
        }

        public GetSensorHubDataModel getSensorHubItemsAll()
        {
            var sensorHubItems = new GetSensorHubDataModel
            {
                items = this.sensorHubItems.ToArray(),
                totalItems = this.sensorHubItems.Count()
            };
            return sensorHubItems;
        }

        public GetSensorHubDataModel getSensorHubItemsFilter(SensorHubDataFilterOptions filters)
        {
            DateTime startDt = Convert.ToDateTime(filters.startDate);
            DateTime limetDt = Convert.ToDateTime(filters.limitDate);

            GetSensorHubDataModel sensorHubItems = new GetSensorHubDataModel();
            if (filters != null)
            {
                sensorHubItems = new GetSensorHubDataModel
                {
                    items = this.sensorHubItems.Take(filters.length).ToArray(),
                    totalItems = filters.length
                };

                if (!string.IsNullOrEmpty(filters.deveui) && filters.startDate != null)
                {
                    IEnumerable<GetSensorHubData> searchItem = new GetSensorHubData[] { };
                    if (filters.limitDate != null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).ToList();
                        }
                    }
                    sensorHubItems.items = searchItem.ToArray();
                    sensorHubItems.totalItems = searchItem.Count();
                }
                else if (!string.IsNullOrEmpty(filters.deveui))
                {
                    IEnumerable<GetSensorHubData> searchItem = new GetSensorHubData[] { };

                    if (filters.length > 0)
                    {
                        searchItem = this.sensorHubItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                    }
                    else
                    {
                        var lastDate = this.sensorHubItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).FirstOrDefault().Date;
                        searchItem = this.sensorHubItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date == lastDate).ToList();
                    }
                    sensorHubItems.items = searchItem.ToArray();
                    sensorHubItems.totalItems = searchItem.Count();
                }
                else if (filters.startDate != null)
                {
                    IEnumerable<GetSensorHubData> searchItem = new GetSensorHubData[] { };
                    if (filters.limitDate == null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.sensorHubItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    sensorHubItems.items = searchItem.ToArray();
                    sensorHubItems.totalItems = searchItem.Count();
                }
                else if (filters.limitDate != null)
                {
                    IEnumerable<GetSensorHubData> searchItem = new GetSensorHubData[] { };
                    if (filters.length > 0)
                    {
                        searchItem = this.sensorHubItemsFilter(startDt.Year, limetDt.Year).Where(x => x.Date <= filters.limitDate).Take(filters.length).ToList();
                    }
                    else
                    {
                        searchItem = this.sensorHubItemsFilter(2020, limetDt.Year).Where(x => x.Date == filters.limitDate).ToList();
                    }
                    sensorHubItems.items = searchItem.ToArray();
                    sensorHubItems.totalItems = searchItem.Count();
                }
            }
            return sensorHubItems;
        }

        public void SensorHubData(LoRaWANDataModel model)
        {
            try
            {
                if(model != null)
                {
                    this.SensorHubDataInsert(model);
                }
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }

        public void SensorHubDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.LAS_501L_Sensor(model.raw_data);
            try
            {
                tbSensorHub sensorHubData = new tbSensorHub()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    Humidity = data.Humidity,
                    Temperature = data.Temperature,
                    CO2 = data.CO2 + 40f,
                    BATVolt = data.BATVolt,
                    BATCurrent = data.BATCurrent,
                    BATLevel = data.BATLevel,
                    BATTemp = data.BATTemp,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbSensorHub.Add(sensorHubData);
                this.db.SaveChanges();

            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }
    }
}