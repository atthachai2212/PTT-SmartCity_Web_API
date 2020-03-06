using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface ILoraWANService
    {
        IEnumerable<tbLoRaWAN> LorawanItems { get; }

        IEnumerable<tbLoRaWAN_RealTime> LorawanRealtimeItems { get; }

        void LorawanData(LorawanServiceModel model);

        void LorawanDataInsert(LorawanServiceModel model);

        void LorawanRealtimeDataInsert(LorawanServiceModel model);

        void LorawanRealtimeDataUpdate(LorawanServiceModel model);
    }
}
