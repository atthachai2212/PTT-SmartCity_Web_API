using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class SensorHubDataModel
    {
        public float Humidity { get; set; }
        public float Temperature { get; set; }
        public float CO2 { get; set; }
        public float? BatteryVolt { get; set; }
        public float? BatteryCurrent { get; set; }
        public float? BatteryPercent { get; set; }
        public float? BatteryTemp { get; set; }

    }
}