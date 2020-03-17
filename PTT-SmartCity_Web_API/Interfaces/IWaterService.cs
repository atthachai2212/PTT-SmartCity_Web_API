using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IWaterService
    {
        IEnumerable<tbWaterLevelSensor> WaterLevelSensorItems { get; }
        IEnumerable<tbWaterQualitySensor> WaterQualitySensorItems { get; }

        void WaterLevelSensorData(LoRaWANDataModel model);

        void WaterLevelSensorDataInsert(LoRaWANDataModel model);

        void WaterQualitySensorData(LoRaWANDataModel model);

        void WaterQualitySensorDataInsert(LoRaWANDataModel model);
    }
}
