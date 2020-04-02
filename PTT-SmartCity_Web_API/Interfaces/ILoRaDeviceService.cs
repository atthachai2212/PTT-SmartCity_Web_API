using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
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

        IEnumerable<string> loraDeviceTypeItems { get; }

        GetLoRaWANDeviceModel GetLoRaWANDevice(LoRaWANDeviceFilterOptions filters);

        string DeviceType(string DevEUI);

        void CreateDevice();

        void UpdateDevice();

        void DeleteDevice();
    }
}
