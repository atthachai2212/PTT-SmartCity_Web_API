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
        IEnumerable<tbSensorHub> sensorHubItems { get; }

        void SensorHubData(LoRaWANDataModel model);

        void SensorHubDataInsert(LoRaWANDataModel model);
    }
}
