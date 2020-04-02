using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface ISensorHubService
    {
        IEnumerable<GetSensorHubData> sensorHubItems { get; }

        IEnumerable<GetSensorHubData> getSensorHubItems { get; }

        GetSensorHubDataModel getSensorHubData();

        GetSensorHubDataModel getSensorHubDataAll();

        GetSensorHubDataModel getSensorHubDataFilter(SensorHubDataFilterOptions filters);

        void SensorHubData(LoRaWANDataModel model);

        void SensorHubDataInsert(LoRaWANDataModel model);
    }
}
