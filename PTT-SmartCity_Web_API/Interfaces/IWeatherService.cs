﻿using PTT_SmartCity_Web_API.Entity;
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
        IEnumerable<GetWeatherData> WeatherSensorItems { get; }

        IEnumerable<GetWeatherData> getWeatherSensor { get; }

        void WeatherSensorData(LoRaWANDataModel model);

        void WeatherSensorDataInsert(LoRaWANDataModel model);

        GetWeatherDataModel getWeatherSensorItems();

        GetWeatherDataModel getWeatherSensorItemsAll();

        GetWeatherDataModel getWeatherSensorItemsFilter(WeatherDataFilterOptions filters);
    }
}
