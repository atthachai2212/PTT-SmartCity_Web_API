using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetEnvironmentAQIData
    {
        public DateTime DateTime { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float O2 { get; set; }
        public float O3 { get; set; }
        public float PM1 { get; set; }
        public float PM2_5 { get; set; }
        public float PM10 { get; set; }
        public float AirTemp { get; set; }
        public float AirHumidity { get; set; }
        public float AirPressure { get; set; }
    }
}