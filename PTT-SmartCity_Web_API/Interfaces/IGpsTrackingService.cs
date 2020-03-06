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

        void GpsData(LorawanServiceModel model);

        void GpsDataInsert(LorawanServiceModel model);

        void GpsRealtimeDataInsert(LorawanServiceModel model);

        void GpsRealtimeDataUpdate(LorawanServiceModel model);
    }
}
