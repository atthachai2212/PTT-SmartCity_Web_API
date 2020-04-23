using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetLoRaDeviceListDataModel
    {
        public GetLoRaDeviceListData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetLoRaDeviceListData
    {
        public string DevEUI { get; set; }
        public string DevAddr { get; set; }
        public string DevType { get; set; }
        public string DevModel { get; set; }
        public string GatewayEUI { get; set; }
        public string Activate { get; set; }
        public string Class { get; set; }
        public string Channel { get; set; }
        public string AppSKey { get; set; }
        public string NwkSkey { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }

    public class LoRaDeviceListDataFilterOptions
    {
        public string deveui { get; set; }
        public int length { get; set; }
    }
}