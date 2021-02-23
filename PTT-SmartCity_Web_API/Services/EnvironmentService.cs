using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class EnvironmentService : IEnvironmentService
    {
        private readonly dbLoRaSmartCityContext db;

        public EnvironmentService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public List<GetEnvironmentData> environmentSensorItemsFilter(int yearDb_start, int yearDb_end)
        {
            var environmentSensorItems = new List<GetEnvironmentData>();
            for (int year = yearDb_start; year <= yearDb_end; year++)
            {
                using (dbLoRaSmartCityContext context = new dbLoRaSmartCityContext(year))
                {
                    var modelEnvi = context.tbEnvironmentSensor.Select(m => new GetEnvironmentData
                    {
                        Date = m.Date,
                        Time = m.Time,
                        DevEUI = m.DevEUI,
                        GatewayEUI = m.GatewayEUI,
                        O2 = m.O2,
                        O3 = m.O3,
                        PM1 = m.PM1,
                        PM2_5 = m.PM2_5,
                        PM10 = m.PM10,
                        AirTemp = m.AirTemp,
                        AirHumidity = m.AirHumidity,
                        AirPressure = m.AirPressure,
                        BATLevel = m.BATLevel,
                        BATVolt = m.BATVolt,
                        RSSI = m.RSSI,
                        SNR = m.SNR
                    }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time).ToList();
                    environmentSensorItems.AddRange(modelEnvi);
                }
            }
            return environmentSensorItems;
        }

        public IEnumerable<GetEnvironmentData> environmentSensorItems => this.db.tbEnvironmentSensor.Select(m => new GetEnvironmentData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            O2 = m.O2,
            O3 = m.O3,
            PM1 = m.PM1,
            PM2_5 = m.PM2_5,
            PM10 = m.PM10,
            AirTemp = m.AirTemp,
            AirHumidity = m.AirHumidity,
            AirPressure = m.AirPressure,
            BATLevel = m.BATLevel,
            BATVolt = m.BATVolt,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetEnvironmentData> getEnvironmentSensor => this.environmentSensorItemsFilter(DateTime.Now.Year - 1, DateTime.Now.Year)
            .OrderByDescending(x => x.Date).ThenByDescending(x => x.Time)
            .GroupBy(m => m.DevEUI, (key, g) => new GetEnvironmentData 
            {
                Date = g.FirstOrDefault().Date,
                Time = g.FirstOrDefault().Time,
                DevEUI = key,
                GatewayEUI = g.FirstOrDefault().GatewayEUI,
                O2 = g.FirstOrDefault().O2,
                O3 = g.FirstOrDefault().O3,
                PM1 = g.FirstOrDefault().PM1,
                PM2_5 = g.FirstOrDefault().PM2_5,
                PM10 = g.FirstOrDefault().PM10,
                AirTemp = g.FirstOrDefault().AirTemp,
                AirHumidity = g.FirstOrDefault().AirHumidity,
                AirPressure = g.FirstOrDefault().AirPressure,
                BATLevel = g.FirstOrDefault().BATLevel,
                BATVolt = g.FirstOrDefault().BATVolt,
                RSSI = g.FirstOrDefault().RSSI,
                SNR = g.FirstOrDefault().SNR
            });

        public void environmentSensorData(LoRaWANDataModel model)
        {
            try
            {
                if(model != null)
                {
                    this.environmentSensorDataInsert(model);
                }
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }

        public void environmentSensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.SEPB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbEnvironmentSensor environmentSensorData = new tbEnvironmentSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    O2 = data.O2conc / 100f,
                    O3 = data.O3conc,
                    PM1 = data.PM1 / 10f,
                    PM2_5 = data.PM2_5 / 10f,
                    PM10 = data.PM10 / 10f,
                    AirTemp = data.TC / 100f,
                    AirHumidity = data.HUM / 100f,
                    AirPressure = data.PRES / 100f,
                    BATLevel = data.BAT,
                    BATVolt = data.BATVolt / 1000f,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbEnvironmentSensor.Add(environmentSensorData);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex.GetErrorException();
            }
        }

        public GetEnvironmentDataModel getEnvironmentSensorItems()
        {
            List<GetEnvironmentData> environmentData = new List<GetEnvironmentData>();
            environmentData.Add(this.getEnvironmentSensor.SingleOrDefault(x => x.DevEUI == "0004a30b0028932c"));
            environmentData.Add(this.getEnvironmentSensor.SingleOrDefault(x => x.DevEUI == "0004a30b00ec0ae2"));
            environmentData.Add(this.getEnvironmentSensor.SingleOrDefault(x => x.DevEUI == "0004a30b00eba418"));
            environmentData.Add(this.getEnvironmentSensor.SingleOrDefault(x => x.DevEUI == "0004a30b0022df4b"));
            environmentData.Add(this.getEnvironmentSensor.SingleOrDefault(x => x.DevEUI == "0004a30b00ebf405"));

            var environmentSensorItems = new GetEnvironmentDataModel
            {
                items = environmentData.ToArray(),
                totalItems = environmentData.Count()
            };

            //var environmentSensorItems = new GetEnvironmentDataModel
            //{
            //    items = this.getEnvironmentSensor.ToArray(),
            //    totalItems = this.getEnvironmentSensor.Count()
            //};
            return environmentSensorItems;
        }


        public GetEnvironmentDataModel getEnvironmentSensorItemsAll()
        {
            var environmentSensorItems = new GetEnvironmentDataModel
            {
                items = this.environmentSensorItemsFilter(2020,DateTime.Now.Year).ToArray(),
                totalItems = this.environmentSensorItemsFilter(2020, DateTime.Now.Year).Count()
            };
            return environmentSensorItems;
        }

        public GetEnvironmentDataModel getEnvironmentSensorItemsFilter(EnvironmentDataFilterOptions filters)
        {
            DateTime startDt = Convert.ToDateTime(filters.startDate);
            DateTime limetDt = Convert.ToDateTime(filters.limitDate);

            GetEnvironmentDataModel environmentSensorItems = new GetEnvironmentDataModel();
            if (filters != null)
            {
                environmentSensorItems = new GetEnvironmentDataModel
                {
                    items = this.environmentSensorItemsFilter(2020, DateTime.Now.Year).Take(filters.length).ToArray(),
                    totalItems = filters.length
                };

                if (!string.IsNullOrEmpty(filters.deveui) && filters.startDate != null)
                {
                    IEnumerable<GetEnvironmentData> searchItem = new GetEnvironmentData[] { };
                    if (filters.limitDate != null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date >= filters.startDate).ToList();
                        }
                    }
                    environmentSensorItems.items = searchItem.ToArray();
                    environmentSensorItems.totalItems = searchItem.Count();
                }
                else if (!string.IsNullOrEmpty(filters.deveui))
                {
                    IEnumerable<GetEnvironmentData> searchItem = new GetEnvironmentData[] { };

                    if (filters.length > 0)
                    {
                        searchItem = this.environmentSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                    }
                    else
                    {
                        var lastDate = this.environmentSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui).FirstOrDefault().Date;
                        searchItem = this.environmentSensorItemsFilter(2020, DateTime.Now.Year).Where(x => x.DevEUI == filters.deveui && x.Date == lastDate).ToList();
                    }
                    environmentSensorItems.items = searchItem.ToArray();
                    environmentSensorItems.totalItems = searchItem.Count();
                }
                else if (filters.startDate != null)
                {
                    IEnumerable<GetEnvironmentData> searchItem = new GetEnvironmentData[] { };
                    if (filters.limitDate == null)
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate).ToList();
                        }
                    }
                    else
                    {
                        if (filters.length > 0)
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).Take(filters.length).ToList();
                        }
                        else
                        {
                            searchItem = this.environmentSensorItemsFilter(startDt.Year, DateTime.Now.Year).Where(x => x.Date >= filters.startDate && x.Date <= filters.limitDate).ToList();
                        }
                    }
                    environmentSensorItems.items = searchItem.ToArray();
                    environmentSensorItems.totalItems = searchItem.Count();
                }
                else if (filters.limitDate != null)
                {
                    IEnumerable<GetEnvironmentData> searchItem = new GetEnvironmentData[] { };
                    if (filters.length > 0)
                    {
                        searchItem = this.environmentSensorItemsFilter(startDt.Year, limetDt.Year).Where(x => x.Date <= filters.limitDate).Take(filters.length).ToList();
                    }
                    else
                    {
                        searchItem = this.environmentSensorItemsFilter(2020, limetDt.Year).Where(x => x.Date == filters.limitDate).ToList();
                    }
                    environmentSensorItems.items = searchItem.ToArray();
                    environmentSensorItems.totalItems = searchItem.Count();
                }
            }
            return environmentSensorItems;
        }
    }
}