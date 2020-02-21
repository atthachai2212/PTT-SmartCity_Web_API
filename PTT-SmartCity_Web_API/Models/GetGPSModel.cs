using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetGPSModel
    {
        public GetGPS[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetGPS
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string Emergency { get; set; }
        public float Battery { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class gpsFilterOptions
    {
        public string searchType { get; set; }
        public string searchText { get; set; }
        public int? length { get; set; }
        public DateTime? startDate { get; set; }
        public DateTime? limitDate { get; set; }

    }
}