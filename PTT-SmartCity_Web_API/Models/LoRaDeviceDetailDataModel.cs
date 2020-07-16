using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class LoRaDeviceDetailDataModel
    {
        public string device_eui { get; set; }
        public string name { get; set; }
        public string details { get; set; }
        public double? latitude { get; set; }
        public double? longitude { get; set; }
    }
}