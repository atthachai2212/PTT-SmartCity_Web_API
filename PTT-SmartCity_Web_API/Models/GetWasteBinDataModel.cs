using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetWasteBinDataModel
    {
        public GetWasteBinData[] items { get; set; }
        public int totalItems { get; set; }
    }
    public class GetWasteBinData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public bool Full { get; set; }
        public bool Flame { get; set; }
        public float AirLevel { get; set; }
        public float? Battery { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class WasteBinDataFilterOptions
    {
        public int start { get; set; }
        public int limit { get; set; }
    }
}