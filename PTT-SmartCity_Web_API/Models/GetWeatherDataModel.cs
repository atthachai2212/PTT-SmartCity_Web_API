﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GetWeatherDataModel
    {
        public int totalItems { get; set; }
        public GetWeatherData[] items { get; set; }
      
    }

    public class GetWeatherData
    {
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public string DevEUI { get; set; }
        public string GatewayEUI { get; set; }
        public float WindSpeed { get; set; }
        public float WindVane { get; set; }
        public float RainfallCurrentHour { get; set; }
        public float RainfallPreviousHour { get; set; }
        public float RainfallLast_24Hours { get; set; }
        public float Luminosity { get; set; }
        public float? BATLevel { get; set; }
        public float? BATVolt { get; set; }
        public float RSSI { get; set; }
        public float SNR { get; set; }
    }

    public class WeatherDataFilterOptions
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