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

        void LorawanData(LoraWANDataModel model);

        void LorawanDataInsert(LoraWANDataModel model);

        void LorawanRealtimeDataInsert(LoraWANDataModel model);

        void LorawanRealtimeDataUpdate(LoraWANDataModel model);
    }
}
