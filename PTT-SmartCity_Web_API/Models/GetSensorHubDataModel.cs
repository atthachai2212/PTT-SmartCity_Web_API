using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetSensorHubDataModel
    {
        public GetSensorHubData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetSensorHubData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float Humidity { get; set; }
        public float Temperature { get; set; }
        public float CO2 { get; set; }
        public float? BATVolt { get; set; }
        public float? BATCurrent { get; set; }
        public float? BATLevel { get; set; }
        public float? BATTemp { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class SensorHubDataFilterOptions
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