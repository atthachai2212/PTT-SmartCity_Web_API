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
        IEnumerable<tbWeatherSensor> WeatherSensorItems { get; }

        void WeatherSensorData(LoRaWANDataModel model);

        void WeatherSensorDataInsert(LoRaWANDataModel model);
    }
}
