using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetEnvironmentDataModel
    {
        public GetEnvironmentData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetEnvironmentData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
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
        public float BATLevel { get; set; }
        public float BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class EnvironmentDataFilterOptions
    {
        public string deveui { get; set; }
        public int length { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? startDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? limitDate { get; set; }
    }
}