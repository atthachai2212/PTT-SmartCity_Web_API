using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Interfaces;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Services
{
    public class WeatherService : IWeatherService
    {
        private dbLoRaSmartCityContext db;

        public WeatherService()
        {
            db = new dbLoRaSmartCityContext();
        }

        public IEnumerable<GetWeatherData> weatherSensorItems => this.db.tbWeatherSensor.Select(m => new GetWeatherData
        {
            Date = m.Date,
            Time = m.Time,
            DevEUI = m.DevEUI,
            GatewayEUI = m.GatewayEUI,
            WindSpeed = m.WindSpeed,
            WindVane = m.WindVane,
            RainfallCurrentHour = m.RainfallCurrentHour,
            RainfallPreviousHour = m.RainfallPreviousHour,
            RainfallLast_24Hours = m.RainfallLast_24Hours,
            Luminosity = m.Luminosity,
            BATVolt = m.BATVolt,
            BATLevel = m.BATLevel,
            RSSI = m.RSSI,
            SNR = m.SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);

        public IEnumerable<GetWeatherData> getWeatherSensor => this.weatherSensorItems.GroupBy(m => m.DevEUI, (key, g) => new GetWeatherData
        {
            Date = g.FirstOrDefault().Date,
            Time = g.FirstOrDefault().Time,
            DevEUI = key,
            GatewayEUI = g.FirstOrDefault().GatewayEUI,
            WindSpeed = g.FirstOrDefault().WindSpeed,
            WindVane = g.FirstOrDefault().WindVane,
            RainfallCurrentHour = g.FirstOrDefault().RainfallCurrentHour,
            RainfallPreviousHour = g.FirstOrDefault().RainfallPreviousHour,
            RainfallLast_24Hours = g.FirstOrDefault().RainfallLast_24Hours,
            Luminosity = g.FirstOrDefault().Luminosity,
            BATVolt = g.FirstOrDefault().BATVolt,
            BATLevel = g.FirstOrDefault().BATLevel,
            RSSI = g.FirstOrDefault().RSSI,
            SNR = g.FirstOrDefault().SNR
        }).OrderByDescending(x => x.Date).ThenByDescending(x => x.Time);


        public GetWeatherDataModel getWeatherSensorItems()
        {
            var weatherSensorItems = new GetWeatherDataModel
            {
                items = this.getWeatherSensor.ToArray(),
                totalItems = this.getWeatherSensor.Count()
            };
            return weatherSensorItems;
        }

        public GetWeatherDataModel getWeatherSensorItemsAll()
        {
            var weatherSensorItems = new GetWeatherDataModel
            {
                items = this.weatherSensorItems.ToArray(),
                totalItems = this.weatherSensorItems.Count()
            };
            return weatherSensorItems;
        }

        public GetWeatherDataModel getWeatherSensorItemsFilter(WeatherDataFilterOptions filters)
        {
            var weatherSensorItems = new GetWeatherDataModel
            {
                items = this.weatherSensorItems.Take(filters.length).ToArray(),
                totalItems = filters.length
            };

            if (!string.IsNullOrEmpty(filters.deveui))
            {
                IEnumerable<GetWeatherData> searchItem = new GetWeatherData[] { };

                if (filters.length > 0)
                {
                    searchItem = this.weatherSensorItems.Where(x => x.DevEUI == filters.deveui).Take(filters.length).ToList();
                }
                else
                {
                    searchItem = this.weatherSensorItems.Where(x => x.DevEUI == filters.deveui).ToList();
                }
                weatherSensorItems.items = searchItem.ToArray();
                weatherSensorItems.totalItems = searchItem.Count();
            }
            return weatherSensorItems;
        }

        public void WeatherSensorData(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            try
            {
                var weatherSensor = this.db.tbWeatherSensor
                    .SingleOrDefault(x => x.Date == dateTime.Date && x.Time.Hours == dateTime.Hour && x.Time.Minutes == dateTime.Minute);
                if (weatherSensor == null)
                {
                    this.WeatherSensorDataInsert(model);
                }
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }

        public void WeatherSensorDataInsert(LoRaWANDataModel model)
        {
            var dateTime = Convert.ToDateTime(model.time);
            var data = LoRaDataService.SAPB_LW_APL_01_Sensor(model.raw_data);
            try
            {
                tbWeatherSensor weatherSensor = new tbWeatherSensor()
                {
                    Date = dateTime.Date,
                    Time = new TimeSpan(dateTime.Hour, dateTime.Minute, dateTime.Second),
                    DevEUI = model.deveui,
                    GatewayEUI = model.gateway_eui,
                    WindSpeed = data.ANE / 100f,
                    WindVane = data.WV / 10f,
                    RainfallCurrentHour = data.PLV1 / 100f,
                    RainfallPreviousHour = data.PLV2 / 100f,
                    RainfallLast_24Hours = data.PLV3 / 10f,
                    Luminosity = Convert.ToUInt16(data.LUX),
                    BATVolt = data.BATVolt / 1000f,
                    BATLevel = data.BAT,
                    RSSI = model.rssi,
                    SNR = model.snr
                };
                this.db.tbWeatherSensor.Add(weatherSensor);
                this.db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex.GetErrorException();
            }
        }
    }
}