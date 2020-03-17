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
        public float? BATVolt { get; set; }
        public float? BATCurrent { get; set; }
        public float? BATLevel { get; set; }
        public float? BATTemp { get; set; }

    }
}