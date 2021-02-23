using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetWaterLevelDataModel
    {
        public GetWaterLevelData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetWaterQualityDataModel
    {
        public GetWaterQualityData[] items { get; set; }
        public int totalItems { get; set; }
    }

    public class GetWaterLevelData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WaterLevel { get; set; }
        public float Distance { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class GetWaterQualityData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WaterTemp { get; set; }
        public float DO { get; set; }
        public float DO_Cal { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class WaterDataFilterOptions
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