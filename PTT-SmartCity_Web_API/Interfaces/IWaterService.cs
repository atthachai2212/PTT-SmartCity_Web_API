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
        IEnumerable<GetWaterLevelData> waterLevelSensorItems { get; }
        IEnumerable<GetWaterQualityData> waterQualitySensorItems { get; }
        IEnumerable<GetWaterLevelData> getWaterLevelSensor { get; }
        IEnumerable<GetWaterQualityData> getWaterQualitySensor { get; }

        List<GetWaterLevelData> waterLevelSensorItemsFilter(int yearDb_start, int yearDb_end);
        List<GetWaterQualityData> waterQualitySensorItemsFilter(int yearDb_start, int yearDb_end);

        void WaterLevelSensorData(LoRaWANDataModel model);

        void WaterLevelSensorDataInsert(LoRaWANDataModel model);

        void WaterQualitySensorData(LoRaWANDataModel model);

        void WaterQualitySensorDataInsert(LoRaWANDataModel model);

        GetWaterLevelDataModel getWaterLevelSensorItems();

        GetWaterLevelDataModel getWaterLevelSensorItemsAll();

        GetWaterLevelDataModel getWaterLevelSensorItemsFilter(WaterDataFilterOptions filters);

        GetWaterQualityDataModel getWaterQualitySensorItems();

        GetWaterQualityDataModel getWaterQualitySensorItemsAll();

        GetWaterQualityDataModel getWaterQualitySensorItemsFilter(WaterDataFilterOptions filters);
    }
}
