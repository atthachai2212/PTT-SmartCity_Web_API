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

        void GpsData(LoraWANDataModel model);

        void GpsDataInsert(LoraWANDataModel model);

        void GpsRealtimeDataInsert(LoraWANDataModel model);

        void GpsRealtimeDataUpdate(LoraWANDataModel model);
    }
}
