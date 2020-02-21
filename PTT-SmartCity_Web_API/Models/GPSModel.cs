using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PTT_SmartCity_Web_API.Models
{
    public class GPSModel
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Time { get; set; }

        [Required]
        public string DevEUI { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        [Required]
        public string Emergency { get; set; }

        [Required]
        public float Battery { get; set; }

        [Required]
        public float RSSI { get; set; }

        [Required]
        public float SNR { get; set; }
    }
}