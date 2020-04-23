using PTT_SmartCity_Web_API.Entity;
using PTT_SmartCity_Web_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTT_SmartCity_Web_API.Interfaces
{
    interface ILoRaDeviceSettingService
    {
        List<GetLoRaDeviceData> loraDeviceItems { get; }

        List<GetLoRaDeviceListData> loraDeviceListItems { get; }

        List<GetLoRaGatewayData> loraGatewayItems { get;  }

        List<string> loraDeviceTypeItems();

        string getDeviceType(string DevEUI);

        void CreateLoRaDevice(GetLoRaDeviceData loraDeviceData);

        void CreateLoRaGateway(GetLoRaGatewayData loraGatewayData);     

        void UpdateLoRaDevice(string DevEUI);

        void DeleteLoRaDevice(string DevEUI);

        void UpdateLoRaGateway(string GatewayEUI);

        void DeleteLoRaGateway(string GatewayEUI);
    }
}
