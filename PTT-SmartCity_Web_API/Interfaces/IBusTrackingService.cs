using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface IBusTrackingService
    {
        IEnumerable<tbGPS_Realtime> GPS_ItemsRealtime { get; }

        IEnumerable<tbGPS> GPS_Items { get; }

        IEnumerable<GPSModel> gpsItemsAll { get;}

        IEnumerable<GPSModel> gpsItemsRealtime { get;}

        GetGPSModel GetGPS(gpsFilterOptions gpsFilters);
      
    }
}
