using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IGpsTrackingService
    {
        IEnumerable<GetGpsData> gpsItems { get; }
        IEnumerable<GetGpsData> gpsRealTimeItems { get; }

        void GpsData(LoRaWANDataModel model);

        void GpsDataInsert(LoRaWANDataModel model);

        void GpsRealtimeDataInsert(LoRaWANDataModel model);

        void GpsRealtimeDataUpdate(LoRaWANDataModel model);

        GetGpsDataModel getGpsItems();

        GetGpsDataModel getGpsItemsAll();

        GetGpsDataModel getGpsItemsFilter(GpsDataFilterOptions filters);
    }
}
