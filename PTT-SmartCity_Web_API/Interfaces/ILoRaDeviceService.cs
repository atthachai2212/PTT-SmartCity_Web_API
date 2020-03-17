using PTT_SmartCity_Web_API.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface ILoRaDeviceService
    {
        IEnumerable<tbLoRaDevice> loraDeviceItems { get; }

        string DeviceType(string DevEUI);

        void AddDevice();

        void UpdateDevice();

        void DeleteDevice();
    }
}
