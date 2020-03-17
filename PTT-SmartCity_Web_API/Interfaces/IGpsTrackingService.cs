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
        IEnumerable<tbGPS> gpsItems { get; }

        IEnumerable<tbGPS_Realtime> gpsRealtimeItems { get; }

        void GpsData(LoRaWANDataModel model);

        void GpsDataInsert(LoRaWANDataModel model);

        void GpsRealtimeDataInsert(LoRaWANDataModel model);

        void GpsRealtimeDataUpdate(LoRaWANDataModel model);
    }
}
