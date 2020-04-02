using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GpsTrackingDataModel
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Emergency { get; set; }
        public int Battery { get; set; }
    }
}