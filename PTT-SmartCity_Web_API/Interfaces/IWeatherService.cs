using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IWeatherService
    {
        IEnumerable<GetWeatherData> weatherSensorItems { get; }

        IEnumerable<GetWeatherData> getWeatherSensor { get; }

        List<GetWeatherData> weatherSensorItemsFilter(int yearDb_start, int yearDb_end);

        void WeatherSensorData(LoRaWANDataModel model);

        void WeatherSensorDataInsert(LoRaWANDataModel model);

        GetWeatherDataModel getWeatherSensorItems();

        GetWeatherDataModel getWeatherSensorItemsAll();

        GetWeatherDataModel getWeatherSensorItemsFilter(WeatherDataFilterOptions filters);
    }
}
