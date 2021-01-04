using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IEnvironmentService
    {
        IEnumerable<GetEnvironmentData> environmentSensorItems { get; }

        List<GetEnvironmentData> environmentSensorItemsFilter(int yearDb_start, int yearDb_end);

        IEnumerable<GetEnvironmentData> getEnvironmentSensor { get; }

        void environmentSensorData(LoRaWANDataModel model);

        void environmentSensorDataInsert(LoRaWANDataModel model);

        GetEnvironmentDataModel getEnvironmentSensorItems();

        GetEnvironmentDataModel getEnvironmentSensorItemsAll();

        GetEnvironmentDataModel getEnvironmentSensorItemsFilter(EnvironmentDataFilterOptions filters);


    }
}
