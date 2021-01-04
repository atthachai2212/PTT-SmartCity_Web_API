using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IWasteBinService
    {
        IEnumerable<GetWasteBinData> wasteBinSensorItems { get; }

        IEnumerable<GetWasteBinData> getWasteBinSensor { get; }

        List<GetWasteBinData> wasteBinSensorItemsFilter (int yearDb_start, int yearDb_end);

        void WasteBinSensorData(LoRaWANDataModel model);

        void WasteBinSensorDataInsert(LoRaWANDataModel model);

        GetWasteBinDataModel getWasteBinSensorItems();

        GetWasteBinDataModel getWasteBinSensorItemsAll();

        GetWasteBinDataModel getWasteBinSensorItemsFilter(WasteBinDataFilterOptions filters);
    }
}
