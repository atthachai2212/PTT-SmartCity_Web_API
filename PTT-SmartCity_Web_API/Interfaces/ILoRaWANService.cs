﻿using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface ILoRaWANService
    {
        IEnumerable<GetLoRaWANData> LorawanItems { get; }

        IEnumerable<GetLoRaWANData> LorawanRealtimeItems { get; }

        GetLoRaWANDataModel GetLoRaWANRealTimeData();

        GetLoRaWANDataModel GetLoRaWANData(LoRaWANDataFilterOptions filters);

        void LorawanData(LoRaWANDataModel model);

        void LorawanDataInsert(LoRaWANDataModel model);

        void LorawanRealtimeDataInsert(LoRaWANDataModel model);

        void LorawanRealtimeDataUpdate(LoRaWANDataModel model);
    }
}
