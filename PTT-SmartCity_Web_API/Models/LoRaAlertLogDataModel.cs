using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class LoRaAlertLogDataModel
    {
        public string date { get; set; }
        public string time { get; set; }
        public string device_eui { get; set; }
        public int alertlevel { get; set; }
        public string details { get; set; }
    }
}